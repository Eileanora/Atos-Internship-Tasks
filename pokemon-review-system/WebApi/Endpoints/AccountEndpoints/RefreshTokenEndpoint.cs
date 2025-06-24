using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using WebApi.Filters;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.AccountEndpoints;

public class RefreshTokenEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/account/refresh-token", async (
                [FromBody] RefreshRequestDto refreshRequest,
                [FromServices] IAuthService authService) =>
        {
            var response = await authService.RefreshTokenAsync(refreshRequest.AccessToken, refreshRequest.RefreshToken);
            return response.ToResults();
        })
        .WithTags(Tags.Account)
        .WithName("RefreshTokenMinimal")
        .AddEndpointFilter<InputValidationEndpointFilter<RefreshRequestDto>>()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
