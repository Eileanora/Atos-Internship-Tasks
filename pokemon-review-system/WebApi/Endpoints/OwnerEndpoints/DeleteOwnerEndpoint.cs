using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using WebApi.Filters;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.OwnerEndpoints;

public class DeleteOwnerEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Owners/{id}", async (
                [FromRoute] string id,
                [FromServices] IOwnerService ownerManager) =>
        {
            var ownerResult = await ownerManager.GetByIdAsync(id);
            if (!ownerResult.IsSuccess)
                return ownerResult.ToResults();
            var result = await ownerManager.DeleteAsync(ownerResult.Value);
            if (!result.IsSuccess)
                return result.ToResults();
            return Results.NoContent();
        })
        .WithName("DeleteOwnerMinimal")
        .WithTags(Tags.Owner)
        .RequireAuthorization()
        // .AddEndpointFilter<AuthOwnerByIdFilter>()
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
    // TODO: Fix filters
}
