namespace Shared.DTOs;

public record CreateOwnerDto : RegisterDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gym { get; set; } = string.Empty;
    public int CountryId { get; set; }
}
