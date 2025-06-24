using Service.DTOs;
using Service.Interfaces;
using Shared.ErrorAndResults;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.PokemonEndpoints;

public class GetPokemonByIdEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/Pokemon/{id}", async (
                IPokemonService pokemonService,
                int id) =>
        {
            var result = await pokemonService.GetByIdAsync(id);
            return result.ToResults();
        })
        .WithName("GetPokemonByIdMinimal")
        .WithTags(Tags.Pokemon)
        .RequireAuthorization("Admin")
        .Produces<PokemonDto>(StatusCodes.Status200OK)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .Produces<Error>(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}
