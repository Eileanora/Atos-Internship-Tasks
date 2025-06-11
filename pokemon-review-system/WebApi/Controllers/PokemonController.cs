using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Helpers.Constants;
using Service.Managers.PokemonManager;

namespace WebApi.Controllers;

[Route("api/Pokemon")]
[ApiController]
public class PokemonController(IPokemonManager pokemonManager) : ControllerBase
{
    // GET ALL ASYNC
    // TODO: add pagination and filtering (use paged list DTO)
    [HttpGet(Name = "GetAllPokemons")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PokemonDto>> GetAllAsync()
    {
        // TODO: ASK WHY ModelState.IsValid IS NEEDED HERE
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var pokemons = await pokemonManager.GetAllAsync();
        return Ok(pokemons);
    }
    
    // GET BY ID ASYNC
    [HttpGet("{id}", Name = "GetPokemonById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PokemonDto>> GetByIdAsync(int id)
    {
        var pokemon = await pokemonManager.GetByIdAsync(id);
        if (pokemon == null)
            return NotFound();
        return Ok(pokemon);
    }
    
    // GET POKEMON RATING ASYNC
    [HttpGet("{id}/rating", Name = "GetPokemonRating")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRatingAsync(int id)
    {
        var rating = await pokemonManager.GetPokemonRatingAsync(id);
        if (rating == -500) // Assuming -500 is a special value indicating not found
            return NotFound();
        return Ok(rating);
    }
    
    // PATCH ASYNC
    [HttpPatch("{id}", Name = "UpdatePokemon")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] PokemonDto pokemonDto)
    {
        pokemonDto.Id = id;
        var (updatedPokemon, errorMessage) = await pokemonManager.UpdateAsync(pokemonDto);
        if (updatedPokemon == null)
        {
            if (!ErrorMessages.CommonErrorMessages.Contains(errorMessage))
                return ValidationProblem("Error updating Pokemon: " + errorMessage);
            
            switch (errorMessage)
            {
                case ErrorMessages.NotFound:
                    return NotFound();
                case ErrorMessages.InternalServerError:
                    return StatusCode(StatusCodes.Status500InternalServerError, ErrorMessages.InternalServerError);
            }
        }
        
        return NoContent();
    }
    
    
    // POST ASYNC
    [HttpPost(Name = "AddPokemon")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAsync([FromBody] PokemonDto pokemonDto)
    {
        var (addedPokemon, errorMessage) = await pokemonManager.AddAsync(pokemonDto);
        if (addedPokemon == null)
        {
            if (!ErrorMessages.CommonErrorMessages.Contains(errorMessage))
                return ValidationProblem("Error adding Pokemon: " + errorMessage);
            
            switch (errorMessage)
            {
                case ErrorMessages.NotFound:
                    return NotFound();
                case ErrorMessages.InternalServerError:
                    return StatusCode(StatusCodes.Status500InternalServerError, ErrorMessages.InternalServerError);
            }
        }
        
        return CreatedAtRoute("GetPokemonById", new { id = addedPokemon.Id }, addedPokemon);
    }
    
    // DELETE ASYNC
    [HttpDelete("{id}", Name = "DeletePokemon")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var pokemon = await pokemonManager.GetByIdAsync(id);
        if (pokemon == null)
            return NotFound();
        
        var success = await pokemonManager.DeleteAsync(pokemon);
        if (!success)
            return BadRequest("Failed to delete the Pokemon.");
        return NoContent();
    }

}

