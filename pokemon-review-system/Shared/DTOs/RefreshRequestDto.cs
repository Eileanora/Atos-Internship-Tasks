namespace Shared.DTOs;

public record RefreshRequestDto
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}
