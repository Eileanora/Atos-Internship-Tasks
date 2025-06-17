namespace Domain.Models;

public interface IAuditFields
{
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}
