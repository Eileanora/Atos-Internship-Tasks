using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Service.Mappers;
using Shared.Helpers;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.PokemonEndpoints;

public class UpdatePokemonEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPatch("/Pokemon/{id}", async (
            int id,
            [FromBody] JsonPatchDocument<PokemonDto> patchDoc,
            IPokemonService pokemonService,
            IValidator<PokemonDto> pokemonValidator) =>
        {
            var pokemonResult = await pokemonService.GetByIdAsync(id);
            if (!pokemonResult.IsSuccess)
                return pokemonResult.ToResults();

            var pokemonToPatch = pokemonResult.Value.ToUpdateDto();
            patchDoc.ApplyTo(pokemonToPatch);

            var inputValid = await ValidationHelper.ValidateAndReportAsync(pokemonValidator, pokemonToPatch, "input");
            if (!inputValid.IsSuccess)
                return inputValid.ToResults();

            pokemonToPatch.Id = id;
            var result = await pokemonService.UpdateAsync(pokemonToPatch);
            if (!result.IsSuccess)
                return result.ToResults();

            return Results.NoContent();
        });
    }
}
