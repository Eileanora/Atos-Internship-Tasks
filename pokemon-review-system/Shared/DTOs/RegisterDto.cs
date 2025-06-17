namespace Shared.DTOs;

public record RegisterDto
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }
}
