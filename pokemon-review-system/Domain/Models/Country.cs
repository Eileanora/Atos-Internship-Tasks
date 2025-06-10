namespace Domain.Models;

public class Country : BaseEntityPrimaryKey
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Owner> Owners { get; set; } = new List<Owner>();

}
