namespace Shared.ResourceParameters;

public class ReviewResourceParameters : BaseResourceParameters
{
    public int? PokemonId { get; set; }
    public int? ReviewerId { get; set; }
    public int? Rating { get; set; }
}
