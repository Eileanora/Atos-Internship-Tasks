using System.Security.Claims;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Service.Common.Constants;
using Service.Interfaces;
using Service.Mappers;
using Shared.DTOs;
using Shared.ErrorAndResults;

namespace Service.Services;

public class AuthService(
    UserManager<User> userManager,
    IGenerateTokenService generateTokenService,
    IUnitOfWork unitOfWork) : IAuthManager
{
    public async Task<Result<User>> RegisterAsync(RegisterDto registerDto)
    {
        var user = registerDto.ToEntity();
        var identityResult = await userManager.CreateAsync(user, registerDto.Password);
        if (!identityResult.Succeeded)
        {
            return Result<User>.Failure(ErrorMessages.InternalServerError);
        }
        return Result<User>.Success(user);
    }

    public async Task<Result<AuthTokensResponse>> LoginAsync(LoginDto userDto)
    {
        var user = await userManager.FindByEmailAsync(userDto.Email);
        var isValid = user != null && await userManager.CheckPasswordAsync(user, userDto.Password);
        if (!isValid)
            return Result<AuthTokensResponse>.Failure(ErrorMessages.WrongCredentials);
        
        var response = await GenerateTokenResponse(user);
        if (!response.IsSuccess)
            return Result<AuthTokensResponse>.Failure(response.Error);
        
        await unitOfWork.RefreshTokenRepository.AddAsync(new RefreshToken
        {
            Token = response.Value.RefreshToken,
            UserId = user.Id,
            ExpiryDate = response.Value.RefreshExpiresAt
        });
        
        var result = await unitOfWork.SaveChangesAsync();
        
        return result <= 0 ? Result<AuthTokensResponse>.Failure(ErrorMessages.InternalServerError) : response;
    }

    public async Task<Result<AuthTokensResponse>> RefreshTokenAsync(string accessToken, string refreshToken)
    {
        var principal = await generateTokenService.GetPrincipalFromExpiredToken(accessToken);
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            await unitOfWork.RefreshTokenRepository.RevokeToken(refreshToken);
            return Result<AuthTokensResponse>.Failure(ErrorMessages.InvalidRefreshToken);
        }
        
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return Result<AuthTokensResponse>.Failure(ErrorMessages.InvalidRefreshToken);
            
        var token = await unitOfWork.RefreshTokenRepository.CheckTokenExistsByUserId(refreshToken, userId);
        if (token == null || token.ExpiryDate < DateTime.UtcNow || token.IsRevoked)
            return Result<AuthTokensResponse>.Failure(ErrorMessages.InvalidRefreshToken);
        
        var response = await GenerateTokenResponse(user);
        if (!response.IsSuccess)
            return Result<AuthTokensResponse>.Failure(response.Error);
        
        unitOfWork.RefreshTokenRepository.ReplaceToken(token, response.Value.RefreshToken, response.Value.RefreshExpiresAt);
        await unitOfWork.SaveChangesAsync();

        return response;
    }

    private async Task<Result<AuthTokensResponse>> GenerateTokenResponse(User user)
    {
        var generatToken = await generateTokenService.GenerateAccessTokenAsync(user);
        var generateRefreshToken = generateTokenService.GenerateRefreshToken();

        if (!generatToken.IsSuccess)
            return Result<AuthTokensResponse>.Failure(generatToken.Error);



        return Result<AuthTokensResponse>.Success(new AuthTokensResponse(
            generatToken.Value,
            generateRefreshToken.Value.Item1,
            generateRefreshToken.Value.Item2));
    }

    public async Task<Result> LogoutAsync(string refreshToken)
    {
        var tokenExists = await unitOfWork.RefreshTokenRepository.CheckTokenExists(refreshToken);
        if (!tokenExists)
            return Result.Failure(ErrorMessages.InvalidRefreshToken);
        
        await unitOfWork.RefreshTokenRepository.RevokeToken(refreshToken);
        await unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
    
    public async Task<Result>AddToRoleAsync(string role, User? sentUser, string? email)
    {
        var user = sentUser;
        if (email != null) // handle use case where this is method is called from assignRole endpoint
            user = await userManager.FindByEmailAsync(email);
        
        // delete previous roles
        var roles = await userManager.GetRolesAsync(user);
        foreach (var r in roles)
        {
            await userManager.RemoveFromRoleAsync(user, r); // TODO: Think about allowing multiple roles
        }
        
        var result = await userManager.AddToRoleAsync(user, role);
        
        return result.Succeeded? Result.Success() : Result.Failure(ErrorMessages.InternalServerError);
    }
}
