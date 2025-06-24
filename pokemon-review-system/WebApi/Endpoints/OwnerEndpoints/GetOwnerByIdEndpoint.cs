using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using WebApi.Filters;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.OwnerEndpoints;

public class GetOwnerByIdEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/Owners/{id}", async (
                [FromServices] IOwnerService ownerManager,
                [FromRoute] string id) =>
        {
            var result = await ownerManager.GetByIdAsync(id);
            return result.ToResults();
        })
        .WithName("GetOwnerByIdMinimal")
        .WithTags(Tags.Owner)
        .RequireAuthorization()
        // .AddEndpointFilter<AuthOwnerByIdFilter>()
        .Produces<OwnerDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}


// TODO: FIX FILTERS
