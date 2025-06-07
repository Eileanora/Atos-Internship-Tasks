namespace WebApi.Models;

public class PokemonOwner
{
    public int PokemonId { get; set; }
    public Pokemon Pokemon { get; set; } = null!;

    public int OwnerId { get; set; }
    public Owner Owner { get; set; } = null!;
}
