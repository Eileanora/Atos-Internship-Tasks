namespace WebApi.Models;

public class Review : BaseEntityPrimaryKey
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Reviewer Reviewer { get; set; } = null!;
    public int ReviewerId { get; set; }
    public Pokemon Pokemon { get; set; } = null!;
    public int PokemonId { get; set; }
}