namespace Domain.Models;

public class RefreshToken : BaseEntity
{
    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsUsed { get; set; }
    // Navigation property
    public virtual User? User { get; set; }
}
