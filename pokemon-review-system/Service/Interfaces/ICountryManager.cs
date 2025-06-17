using Shared.DTOs;

namespace Service.Interfaces;

public interface ICountryManager
{
    Task<IEnumerable<CountryDto>> GetAllAsync();
}