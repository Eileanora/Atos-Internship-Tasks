using Domain.Interfaces;
using Service.Interfaces;
using Service.Mappers;
using Shared.DTOs;

namespace Service.Services;

public class CountryService(IUnitOfWork unitOfWork) : ICountryManager
{
    // GET ALL ASYNC
    public async Task<IEnumerable<CountryDto>> GetAllAsync()
    {
        var countries = await unitOfWork.CountryRepository.GetAllAsync();
        return countries.Select(c => c.ToListDto());
    }
}
