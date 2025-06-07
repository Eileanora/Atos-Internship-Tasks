namespace WebApi.Models;

public class PokemonCategory
{
    public int PokemonId { get; set; }
    public Pokemon Pokemon { get; set; } = null!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}