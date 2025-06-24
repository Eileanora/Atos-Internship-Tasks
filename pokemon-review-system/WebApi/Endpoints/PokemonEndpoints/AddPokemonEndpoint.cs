using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Shared.ErrorAndResults;
using Shared.Helpers;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.PokemonEndpoints;

public class AddPokemonEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/Pokemon", async (
                [FromBody] PokemonDto pokemonDto,
                [FromServices] IPokemonService pokemonService,
                [FromServices] IValidator<PokemonDto> pokemonValidator) =>
            {
                var inputValid = await ValidationHelper.ValidateAndReportAsync(pokemonValidator, pokemonDto, "Input");
                if (!inputValid.IsSuccess)
                    return inputValid.ToResults();
                var result = await pokemonService.AddAsync(pokemonDto);
                if (!result.IsSuccess)
                    return result.ToResults();
                return Results.CreatedAtRoute("GetPokemonByIdMinimal", new { id = result.Value.Id }, result.Value);
            })
            .WithName("AddPokemonMinimal")
            .WithTags(Tags.Pokemon)
            .RequireAuthorization("Admin")
            .Produces<PokemonDto>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces<Error>(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}
