using FluentValidation;
using Shared.Helpers;

namespace WebApi.Endpoints.PokemonEndpoints;

public static class PokemonEndpoints
{
    public static void MapPokemonEndpoints(this IEndpointRouteBuilder app)
    {
        new GetAllPokemonsEndpoint().MapEndpoints(app);
        new AddPokemonEndpoint().MapEndpoints(app);
        new GetPokemonByIdEndpoint().MapEndpoints(app);
        new GetPokemonRatingEndpoint().MapEndpoints(app);
        new DeletePokemonEndpoint().MapEndpoints(app);
        // If you add more endpoints, instantiate and call MapEndpoints here.
    }
}
