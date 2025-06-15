using Shared.DTOs;

namespace Service.Managers.CountryManager;

public interface ICountryManager
{
    Task<IEnumerable<CountryDto>> GetAllAsync();
}