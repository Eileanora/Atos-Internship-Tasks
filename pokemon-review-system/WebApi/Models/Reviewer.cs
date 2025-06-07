namespace WebApi.Models;

public class Reviewer : BaseEntityPrimaryKey
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
