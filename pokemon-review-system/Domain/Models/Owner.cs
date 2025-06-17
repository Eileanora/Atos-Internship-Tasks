namespace Domain.Models;

public class Owner : BaseEntityPrimaryKey
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gym { get; set; } = string.Empty;
    public Country? Country { get; set; }
    public int CountryId { get; set; }

    // public ICollection<Pokemon>? Pokemons { get; set; } = new List<Pokemon>();
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
    
    public string UserId { get; set; } = string.Empty;
    public virtual User User { get; set; }
}
