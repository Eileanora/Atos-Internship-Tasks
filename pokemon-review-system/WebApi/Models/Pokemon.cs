namespace WebApi.Models;

public class Pokemon : BaseEntityPrimaryKey
{
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<PokemonCategory> PokemonCategories { get; set; } = new List<PokemonCategory>();
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
}
