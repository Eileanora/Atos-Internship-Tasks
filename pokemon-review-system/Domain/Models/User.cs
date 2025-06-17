using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User : IdentityUser
{
    public ICollection<RefreshToken> RefreshTokens = new List<RefreshToken>();
}
