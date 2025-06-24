using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using WebApi.Filters;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.AccountEndpoints;

public class LoginEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/account/login", async (
                [FromBody] LoginDto model,
                [FromServices] IAuthService authService) =>
        {
            var response = await authService.LoginAsync(model);
            return response.ToResults();
        })
        .WithTags(Tags.Account)
        .WithName("LoginMinimal")
        .AddEndpointFilter<InputValidationEndpointFilter<LoginDto>>()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
