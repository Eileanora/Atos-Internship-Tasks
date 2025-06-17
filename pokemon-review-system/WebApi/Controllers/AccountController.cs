using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Service.Managers.AuthManager;
using Shared.DTOs;
using WebApi.Helpers.Extensions;

namespace WebApi.Controllers;


[Route("api/account")]
[ApiController]
public class AccountController(
    IAuthManager authManager) : ControllerBase
{
    // Login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var response = await authManager.LoginAsync(model);
        return response.ToActionResult();
    }
    
    // Logout
    [HttpPost("logout")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest logoutRequest)
    {
        var result = await authManager.LogoutAsync(logoutRequest.RefreshToken);
        return result.ToActionResult();
    }
    
    // Refresh
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshRequestDto refreshRequest)
    {
        if (string.IsNullOrEmpty(refreshRequest.AccessToken) || string.IsNullOrEmpty(refreshRequest.RefreshToken))
            return BadRequest("Access token and refresh token are required.");

        var response = await authManager.RefreshTokenAsync(refreshRequest.AccessToken, refreshRequest.RefreshToken);
        return response.ToActionResult();
    }
    
    [HttpPost("add-role")]
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public async Task<IActionResult> AddRole([FromBody] AddRoleRequest request)
    {
        await authManager.AddToRoleAsync(request.Role, null, request.Email);
        return Ok();
    }
}

public record LogoutRequest
{
    public string RefreshToken { get; init; }
}

public record AddRoleRequest
{
    public string Email { get; init; }
    public string Role { get; init; }
}
