using Domain.Models;
using Service.DTOs;
using Shared.ErrorAndResults;

namespace Service.Interfaces;

public interface IAuthService
{
    Task<Result<User>> RegisterAsync(RegisterDto registerDto);
    Task<Result<AuthTokensResponse>> LoginAsync(LoginDto userDto);
    Task<Result<AuthTokensResponse>> RefreshTokenAsync(string accessToken, string refreshToken);
    public Task<Result> LogoutAsync(string refreshToken);
    
    Task<Result> AddToRoleAsync(string role, User? user, string? email);
}
