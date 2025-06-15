namespace Service.Common.ResourceParameters;

public class PokemonResourceParameters : BaseResourceParameters
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Region { get; set; }
    public int? MinHeight { get; set; }
    public int? MaxHeight { get; set; }
    public int? MinWeight { get; set; }
    public int? MaxWeight { get; set; }
    public bool? IsLegendary { get; set; }
    
    // Additional properties can be added as needed
}
