using System.Text.Json.Serialization;

namespace Service.DTOs;

public class ReviewDto : BaseDto
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Content { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ReviewerId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ReviewerName { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PokemonId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PokemonName { get; set; }
    public int? Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}
