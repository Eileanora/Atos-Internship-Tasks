using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace WebApi.Controllers;

[Route("api/Countries")]
[ApiController]
public class CountryController(ICountryService countryService) : ControllerBase
{
    // GET ALL ASYNC
    [HttpGet(Name = "GetAllCountries")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<string>>> GetAllAsync()
    {
        var countries = await countryService.GetAllAsync();
        return Ok(countries);
    }
}
