namespace Domain.Models;

public class Category : BaseEntityPrimaryKey
{
    public string Name { get; set; } = string.Empty;
    public ICollection<PokemonCategory> PokemonCategories { get; set; } = new List<PokemonCategory>();
}