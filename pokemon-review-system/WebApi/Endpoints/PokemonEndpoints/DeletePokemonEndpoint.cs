using Service.DTOs;
using Service.Interfaces;
using Shared.ErrorAndResults;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.PokemonEndpoints;

public class DeletePokemonEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Pokemon/{id}", async (
                int id,
                IPokemonService pokemonService) =>
            {
                var pokemonResult = await pokemonService.GetByIdAsync(id);
                if (!pokemonResult.IsSuccess)
                    return pokemonResult.ToResults();
                var result = await pokemonService.DeleteAsync(pokemonResult.Value);
                if (!result.IsSuccess)
                    return result.ToResults();
                return Results.NoContent();
            })
            .WithName("DeletePokemonMinimal")
            .RequireAuthorization("Admin")
            .Produces<Error>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces<Error>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(Tags.Pokemon);
    }
}
