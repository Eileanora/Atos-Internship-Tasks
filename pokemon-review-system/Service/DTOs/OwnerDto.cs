using System.Text.Json.Serialization;

namespace Service.DTOs;

public class OwnerDto : BaseDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public int? HiddenId { get; set; }
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Gym { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CountryId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CountryName { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<int>? PokemonIds { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PokemonCount { get; set; }
}
