using Service.Interfaces;

namespace WebApi.Endpoints.CountryEndpoints;

public class GetAllCountriesEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/Countries", async (ICountryService countryService) =>
        {
            var countries = await countryService.GetAllAsync();
            return Results.Ok(countries);
        })
        .WithName(Tags.Country)
        .WithName("GetAllCountriesMinimal")
        .Produces<IEnumerable<string>>(StatusCodes.Status200OK);
    }
}
