using Microsoft.AspNetCore.Mvc;
using Service.Managers.CountryManager;

namespace WebApi.Controllers;

[Route("api/Countries")]
[ApiController]
public class CountryController(ICountryManager countryManager) : ControllerBase
{
    // GET ALL ASYNC
    [HttpGet(Name = "GetAllCountries")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<string>>> GetAllAsync()
    {
        var countries = await countryManager.GetAllAsync();
        return Ok(countries);
    }
}
