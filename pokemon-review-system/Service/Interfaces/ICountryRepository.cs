namespace Service.Interfaces;
using Domain.Models;

public interface ICountryRepository : IReadOnlyBaseRepository<Country>
{
    Task<bool> ExistsAsync(int id);
}
