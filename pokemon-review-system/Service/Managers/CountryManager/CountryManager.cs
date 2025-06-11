using Service.DTOs;
using Service.Interfaces;
using Service.Mappers;

namespace Service.Managers.CountryManager;

public class CountryManager(ICountryRepository countryRepository) : ICountryManager
{
    // GET ALL ASYNC
    public async Task<IEnumerable<CountryDto>> GetAllAsync()
    {
        var countries = await countryRepository.GetAllAsync();
        return countries.Select(c => c.ToListDto());
    }
}
