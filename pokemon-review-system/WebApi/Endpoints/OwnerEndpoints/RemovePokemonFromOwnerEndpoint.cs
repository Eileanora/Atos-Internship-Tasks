using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using WebApi.Filters;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.OwnerEndpoints;

public class RemovePokemonFromOwnerEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Owners/{ownerId}/pokemons/{pokemonId}", async (
                [FromRoute] string ownerId,
                [FromRoute] int pokemonId,
                [FromServices] IOwnerService ownerManager) =>
        {
            var authenticationResult = await ownerManager.ValidateOwnerAuthentication(ownerId);
            if (!authenticationResult.IsSuccess)
                return authenticationResult.ToResults();
            var result = await ownerManager.RemovePokemonFromOwnerAsync(ownerId, pokemonId);
            if (!result.IsSuccess)
                return result.ToResults();
            return Results.NoContent();
        })
        .WithName("RemovePokemonFromOwnerMinimal")
        .WithTags(Tags.Owner)
        .RequireAuthorization()
        // .AddEndpointFilter<AuthOwnerByIdFilter>()
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
    // TODO: FIX FILTERS
}
