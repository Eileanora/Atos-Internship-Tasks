using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Service.Helpers;

public sealed class UserContext(IHttpContextAccessor httpContextAccessor)
    : IUserContext
{
    public Guid UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");

    public bool IsAuthenticated =>
        httpContextAccessor
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ??
        throw new ApplicationException("User context is unavailable");

    public bool IsAdmin => 
        httpContextAccessor
            .HttpContext?
            .User
            .IsInRole("Admin") ??
        throw new ApplicationException("User context is unavailable");
}
