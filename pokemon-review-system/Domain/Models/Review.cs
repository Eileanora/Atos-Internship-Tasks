namespace Domain.Models;

public class Review : BaseEntityPrimaryKey
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public Reviewer? Reviewer { get; set; }
    public int ReviewerId { get; set; }
    public Pokemon? Pokemon { get; set; }
    public int PokemonId { get; set; }
}