namespace WebApi.Models;

public class Pokemon : BaseEntityPrimaryKey
{
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}