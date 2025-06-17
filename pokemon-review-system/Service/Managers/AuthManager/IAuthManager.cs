using Domain.Models;
using Shared.DTOs;
using Shared.ErrorAndResults;

namespace Service.Managers.AuthManager;

public interface IAuthManager
{
    Task<Result<User>> RegisterAsync(RegisterDto registerDto);
    Task<Result?> LoginAsync(LoginDto userDto);
    Task<Result?> RefreshTokenAsync(string accessToken, string refreshToken);
    public Task<bool> LogoutAsync(string refreshToken);
}
