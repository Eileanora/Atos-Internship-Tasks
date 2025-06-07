namespace WebApi.Models;

public class Owner : BaseEntityPrimaryKey
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gym { get; set; } = string.Empty;
    public Country Country { get; set; } = new Country();
    public int CountryId { get; set; }
    
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
}