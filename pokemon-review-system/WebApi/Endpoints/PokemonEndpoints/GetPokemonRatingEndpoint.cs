using Service.Interfaces;
using Shared.ErrorAndResults;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.PokemonEndpoints;

public class GetPokemonRatingEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/Pokemon/{id}/rating", async (
                IPokemonService pokemonService,
                int id) =>
            {
                var result = await pokemonService.GetPokemonRatingAsync(id);
                return result.ToResults();
            })
            .WithName("GetPokemonRatingMinimal")
            .WithTags(Tags.Pokemon)
            .Produces<Error>(StatusCodes.Status404NotFound)
            .Produces<decimal>(StatusCodes.Status200OK);
    }
}
