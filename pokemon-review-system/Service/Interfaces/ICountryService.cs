using Service.DTOs;

namespace Service.Interfaces;

public interface ICountryService
{
    Task<IEnumerable<CountryDto>> GetAllAsync();
}