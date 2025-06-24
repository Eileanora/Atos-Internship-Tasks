using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using WebApi.Filters;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.AccountEndpoints;

public class LogoutEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/account/logout", async (
                [FromBody] LogoutRequest logoutRequest,
                [FromServices] IAuthService authService) =>
        {
            var result = await authService.LogoutAsync(logoutRequest.RefreshToken);
            return result.ToResults();
        })
        .WithTags(Tags.Account)
        .WithName("LogoutMinimal")
        .AddEndpointFilter<InputValidationEndpointFilter<LogoutRequest>>()
        .RequireAuthorization()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}

