using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Service.Managers.PokemonManager;
using Service.Mappers;
using WebApi.Helpers.Extensions;
using WebApi.Helpers.Validation;

namespace WebApi.Controllers;

[Route("api/Pokemon")]
[ApiController]
public class PokemonController(IPokemonManager pokemonManager,
    IValidator<PokemonDto> pokemonValidator) : ControllerBase
{
    // GET ALL ASYNC
    // TODO: add pagination and filtering (use paged list DTO)
    [HttpGet(Name = "GetAllPokemons")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var pokemons = await pokemonManager.GetAllAsync();
        return pokemons.ToActionResult();
    }
    
    // GET BY ID ASYNC
    [HttpGet("{id}", Name = "GetPokemonById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await pokemonManager.GetByIdAsync(id);
        return result.ToActionResult();
    }
    
    // GET POKEMON RATING ASYNC
    [HttpGet("{id}/rating", Name = "GetPokemonRating")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRatingAsync(int id)
    {
        var result = await pokemonManager.GetPokemonRatingAsync(id);
        return result.ToActionResult();
    }
    
    // PATCH ASYNC
    [HttpPatch("{id}", Name = "UpdatePokemon")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync(int id, JsonPatchDocument<PokemonDto> patchDoc)
    {
        var pokemonResult = await pokemonManager.GetByIdAsync(id);
        if (!pokemonResult.IsSuccess)
            return pokemonResult.ToActionResult();

        var pokemonToPatch = pokemonResult.Value.ToUpdateDto();
        var (patchedDto, validationResult) = this.HandlePatch(pokemonToPatch, patchDoc);
        if (!validationResult.IsSuccess)
            return validationResult.ToActionResult();
        
        var inputValid = await ValidationHelper.ValidateAndReportAsync(pokemonValidator, patchedDto, "input");
        if (!inputValid.IsSuccess)
            return inputValid.ToActionResult();

        patchedDto.Id = id;
        var result = await pokemonManager.UpdateAsync(pokemonToPatch);
        if (!result.IsSuccess)
            return result.ToActionResult();

        return NoContent();
    }

    // POST ASYNC
    [HttpPost(Name = "AddPokemon")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAsync([FromBody] PokemonDto pokemonDto)
    {
        var inputValid = await ValidationHelper.ValidateAndReportAsync(pokemonValidator, pokemonDto, "Input");
        if (!inputValid.IsSuccess)
            return inputValid.ToActionResult();
        var result = await pokemonManager.AddAsync(pokemonDto);
        if (!result.IsSuccess)
            return result.ToActionResult();
        return CreatedAtRoute("GetPokemonById", new { id = result.Value.Id }, result.Value);
    }
    
    // DELETE ASYNC
    [HttpDelete("{id}", Name = "DeletePokemon")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var pokemonResult = await pokemonManager.GetByIdAsync(id);
        if (!pokemonResult.IsSuccess)
            return pokemonResult.ToActionResult();
        var result = await pokemonManager.DeleteAsync(pokemonResult.Value);
        if (!result.IsSuccess)
            return result.ToActionResult();
        return NoContent();
    }
}
