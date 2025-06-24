using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using WebApi.Filters;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.AccountEndpoints;

public class AddRoleEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/account/add-role", async (
                [FromBody] AddRoleRequest request,
                [FromServices] IAuthService authService) =>
        {
            var result = await authService.AddToRoleAsync(request.Role, null, request.Email);
            return result.ToResults();
        })
        .WithTags(Tags.Account)
        .WithName("AddRoleMinimal")
        .AddEndpointFilter<InputValidationEndpointFilter<AddRoleRequest>>()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
