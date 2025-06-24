using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Shared.ErrorAndResults;
using Shared.ResourceParameters;
using WebApi.Helpers.Extensions;
using WebApi.Helpers.PaginationHelper;

namespace WebApi.Endpoints.PokemonEndpoints;

public class GetAllPokemonsEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/Pokemon", async (
                [AsParameters] PokemonResourceParameters resourceParameters,
                IPokemonService pokemonService,
                IPaginationHelper<PokemonDto, PokemonResourceParameters> paginationHelper,
                IHttpContextAccessor context,
                [FromServices] IUrlHelper urlHelper) =>
            {
                var pokemons = await pokemonService.GetAllAsync(resourceParameters);
                paginationHelper
                    .CreateMetaDataHeader(
                        pokemons.Value, resourceParameters,
                        context.HttpContext.Response.Headers,
                        urlHelper,
                        "GetAllPokemonsMinimal");
                return pokemons.ToResults();
            }).WithName("GetAllPokemonsMinimalMinimal")
            .WithTags(Tags.Pokemon)
            .Produces<PagedResult<PokemonDto>>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status500InternalServerError);
    }
}