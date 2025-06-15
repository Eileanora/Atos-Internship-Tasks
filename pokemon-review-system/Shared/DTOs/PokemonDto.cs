namespace Shared.DTOs;
using System.Text.Json.Serialization;

public class PokemonDto : BaseDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public DateTime? BirthDate { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<int>? CategoriesId { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<CategoryDto>? Categories { get; set; }
}
