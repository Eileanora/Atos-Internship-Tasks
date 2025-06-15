namespace Shared.DTOs;

public class PokemonDto : BaseDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public List<CategoryDto>? Categories { get; set; }
}
