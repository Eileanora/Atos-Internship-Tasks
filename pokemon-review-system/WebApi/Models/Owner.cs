namespace WebApi.Models;

public class Owner : BaseEntityPrimaryKey
{
    public string Name { get; set; } = string.Empty;
    public string Gym { get; set; } = string.Empty;
}