using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Service.Common.Constants;
using Service.Mappers;
using Shared.DTOs;
using Shared.ErrorAndResults;

namespace Service.Managers.AuthManager;

public class AuthManager(
    UserManager<User> userManager) : IAuthManager
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

    public Task<Result?> LoginAsync(LoginDto userDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result?> RefreshTokenAsync(string accessToken, string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> LogoutAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }
}