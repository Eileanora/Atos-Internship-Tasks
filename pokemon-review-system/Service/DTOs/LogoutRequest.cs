namespace Service.DTOs;

public record LogoutRequest
{
    public string RefreshToken { get; init; }
}