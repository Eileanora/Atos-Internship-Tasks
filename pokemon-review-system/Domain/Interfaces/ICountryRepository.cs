using Domain.Models;

namespace Domain.Interfaces;

public interface ICountryRepository : IReadOnlyBaseRepository<Country>
{
    Task<bool> ExistsAsync(int id);
}
