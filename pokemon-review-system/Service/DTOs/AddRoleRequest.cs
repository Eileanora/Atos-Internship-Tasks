namespace Service.DTOs;

public record AddRoleRequest
{
    public string Email { get; init; }
    public string Role { get; init; }
}