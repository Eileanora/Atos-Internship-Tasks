using Shared.DTOs;
using Service.Interfaces;
using Service.Mappers;

namespace Service.Managers.CountryManager;

public class CountryManager(IUnitOfWork unitOfWork) : ICountryManager
{
    // GET ALL ASYNC
    public async Task<IEnumerable<CountryDto>> GetAllAsync()
    {
        var countries = await unitOfWork.CountryRepository.GetAllAsync();
        return countries.Select(c => c.ToListDto());
    }
}
