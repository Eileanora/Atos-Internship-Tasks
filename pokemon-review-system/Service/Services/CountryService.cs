using Domain.Interfaces;
using Service.DTOs;
using Service.Interfaces;
using Service.Mappers;

namespace Service.Services;

public class CountryService(IUnitOfWork unitOfWork) : ICountryService
{
    // GET ALL ASYNC
    public async Task<IEnumerable<CountryDto>> GetAllAsync()
    {
        var countries = await unitOfWork.CountryRepository.GetAllAsync();
        return countries.Select(c => c.ToListDto());
    }
}
