using Domain.Models;

namespace Service.Interfaces;

public interface IPokemonRepository : IBaseRepository<Pokemon>
{
    Task<bool> ExistsAsync(int id);
    Task<Pokemon?> GetByIdAsync(int id);
    Task<Pokemon?> GetByNameAsync(string name);
    Task<Decimal> GetPokemonRatingAsync(int id);
    Task <bool> CheckNameUniqueAsync(string name);
}
