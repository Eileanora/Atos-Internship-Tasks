using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

/*
 * This attempt failed to work because of the database design, ownerId is not the same as userId
 * and in this endpoint, we work with ownerId, not userId which is not accessible in this context.
 */

public class AuthOwnerByIdFilter(
    IUserContext userContext) : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var isAuthenticated = userContext.IsAuthenticated;
        
        if (!isAuthenticated)
        {
            context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
            return;
        }
        var userId = userContext.UserId;
        var isAdmin = userContext.IsAdmin;
        
        if (context.ActionArguments.TryGetValue("id", out var idObj) && idObj is Guid id)
        {
            if (userId != id && !isAdmin)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
            }
        }
        // allow the movement of the request to the next middleware
        await next();
    }
}
