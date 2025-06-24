using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using WebApi.Filters;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.OwnerEndpoints;

public class AddPokemonToOwnerEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/Owners/{ownerId}/pokemons/{pokemonId}", async (
                [FromRoute] string ownerId,
                [FromRoute] int pokemonId,
                [FromServices] IOwnerService ownerManager) =>
        {
            var authenticationResult = await ownerManager.ValidateOwnerAuthentication(ownerId);
            if (!authenticationResult.IsSuccess)
                return authenticationResult.ToResults();
            var result = await ownerManager.AddPokemonToOwnerAsync(ownerId, pokemonId);
            if (!result.IsSuccess)
                return result.ToResults();
            return Results.NoContent();
        })
        .WithName("AddPokemonToOwnerMinimal")
        .RequireAuthorization()
        // .AddEndpointFilter<AuthOwnerByIdFilter>()
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags(Tags.Owner);
    }
    // TODO: Fix Filters
}
