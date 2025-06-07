namespace WebApi.Models;

public class Country : BaseEntityPrimaryKey
{
    public string Name { get; set; } = string.Empty;
}