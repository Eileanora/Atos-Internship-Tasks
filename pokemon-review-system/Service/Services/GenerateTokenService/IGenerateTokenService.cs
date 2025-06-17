using System.Security.Claims;
using Domain.Models;
using Shared.ErrorAndResults;

namespace Service.Services.GenerateTokenService;

public interface IGenerateTokenService
{
    Task<Result<string>> GenerateAccessTokenAsync(User user);
    Result<(string, DateTime)> GenerateRefreshToken();
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
}
