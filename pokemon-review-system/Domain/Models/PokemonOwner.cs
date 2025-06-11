namespace Domain.Models;

public class PokemonOwner : BaseEntity
{
    public int PokemonId { get; set; }
    public Pokemon? Pokemon { get; set; }

    public int OwnerId { get; set; }
    public Owner? Owner { get; set; }
}
