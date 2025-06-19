using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;

namespace WebApi.Controllers;

[Route("api/Category")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    // GET ALL ASYNC
    [HttpGet(Name = "GetAllCategories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllAsync()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var categories = await categoryService.GetAllAsync();
        return Ok(categories);
    }
    
    // GET BY ID ASYNC
    [HttpGet("{id}", Name = "GetCategoryById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> GetByIdAsync(int id)
    {
        var category = await categoryService.GetByIdAsync(id);
        if (category == null)
            return NotFound();
        return Ok(category);
    }
    
    // TODO: DO THIS IN GET ALL POKEMONS AS A FILTER
    // // GET POKEMON BY CATEGORY ID ASYNC???
    // [HttpGet("pokemon/{CategoryId}", Name = "GetPokemonsByCategoryId")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // // POST ASYNC
}