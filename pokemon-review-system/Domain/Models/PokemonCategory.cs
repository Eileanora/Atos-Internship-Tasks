namespace Domain.Models;

public class PokemonCategory : BaseEntity
{
    public int PokemonId { get; set; }
    public Pokemon? Pokemon { get; set; }

    public int CategoryId { get; set; }
    
    // TODO: CHECK DIFF BETWEEN BOTH AND WHICH TO USE
    public Category? Category { get; set; }

    // public Category Category { get; set; } = null!;
}