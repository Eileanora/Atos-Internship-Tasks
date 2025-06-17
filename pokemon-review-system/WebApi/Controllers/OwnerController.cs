using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Managers.OwnerManager;
using Shared.DTOs;
using Shared.Helpers;
using Shared.ResourceParameters;
using WebApi.Helpers.Extensions;
using WebApi.Helpers.PaginationHelper;
using WebApi.Helpers.Validation;

namespace WebApi.Controllers;

[Route("api/Owners")]
[ApiController]
public class OwnerController(IOwnerManager ownerManager,
    IValidator<OwnerDto> ownerValidator,
    IValidator<CreateOwnerDto> createOwnerValidator,
    IPaginationHelper<OwnerDto, OwnerResourceParameters> paginationHelper) : ControllerBase
{
    // GET ALL ASYNC
    [HttpGet(Name = "GetAllOwners")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] OwnerResourceParameters resourceParameters)
    {
        var owners = await ownerManager.GetAllAsync(resourceParameters);
        paginationHelper
            .CreateMetaDataHeader(
                owners.Value, resourceParameters, Response.Headers, Url, "GetAllOwners");
        return owners.ToActionResult();
    }

    // GET BY ID ASYNC
    [HttpGet("{id}", Name = "GetOwnerById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await ownerManager.GetByIdAsync(id);
        return result.ToActionResult();
    }

    // POST ASYNC
    [HttpPost(Name = "AddOwner")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAsync([FromBody] CreateOwnerDto ownerDto)
    {
        var inputValid = await ValidationHelper.ValidateAndReportAsync(createOwnerValidator, ownerDto, "Input");
        if (!inputValid.IsSuccess)
            return inputValid.ToActionResult();
        var result = await ownerManager.AddAsync(ownerDto);
        if (!result.IsSuccess)
            return result.ToActionResult();
        return Created();
    }
    
    // PATCH ASYNC
    [HttpPatch("{id}", Name = "UpdateOwner")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync(int id, JsonPatchDocument<OwnerDto> patchDoc)
    {
        var ownerResult = await ownerManager.GetByIdAsync(id);
        if (!ownerResult.IsSuccess)
            return ownerResult.ToActionResult();

        var ownerToPatch = ownerResult.Value; // Assuming ToUpdateDto is not needed for OwnerDto
        var (patchedDto, validationResult) = this.HandlePatch(ownerToPatch, patchDoc);
        if (!validationResult.IsSuccess)
            return validationResult.ToActionResult();

        var inputValid = await ValidationHelper.ValidateAndReportAsync(ownerValidator, patchedDto, "input");
        if (!inputValid.IsSuccess)
            return inputValid.ToActionResult();

        patchedDto.Id = id;
        var result = await ownerManager.UpdateAsync(patchedDto);
        if (!result.IsSuccess)
            return result.ToActionResult();

        return NoContent();
    }

    // DELETE ASYNC
    [HttpDelete("{id}", Name = "DeleteOwner")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var ownerResult = await ownerManager.GetByIdAsync(id);
        if (!ownerResult.IsSuccess)
            return ownerResult.ToActionResult();
        var result = await ownerManager.DeleteAsync(ownerResult.Value);
        if (!result.IsSuccess)
            return result.ToActionResult();
        return NoContent();
    }
// TODO: pass pokemonId as a query parameter instead of route parameter     
    // ADD POKEMON TO OWNER
    [HttpPost("{ownerId}/pokemons/{pokemonId}", Name = "AddPokemonToOwner")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddPokemonToOwnerAsync(int ownerId, int pokemonId)
    {
        var result = await ownerManager.AddPokemonToOwnerAsync(ownerId, pokemonId);
        if (!result.IsSuccess)
            return result.ToActionResult();
        return NoContent();
    }

    // REMOVE POKEMON FROM OWNER
    [HttpDelete("{ownerId}/pokemons/{pokemonId}", Name = "RemovePokemonFromOwner")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemovePokemonFromOwnerAsync(int ownerId, int pokemonId)
    {
        var result = await ownerManager.RemovePokemonFromOwnerAsync(ownerId, pokemonId);
        if (!result.IsSuccess)
            return result.ToActionResult();
        return NoContent();
    }
}
