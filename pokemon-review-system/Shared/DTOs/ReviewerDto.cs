using System.Text.Json.Serialization;

namespace Shared.DTOs;

public class ReviewerDto : BaseDto
{
    public int? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ReviewCount { get; set; }
}
