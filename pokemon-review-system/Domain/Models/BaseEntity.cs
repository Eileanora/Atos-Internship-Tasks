namespace Domain.Models;

public class BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    // Null because i won't bother changing existing data in database
    public Guid? CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
}
