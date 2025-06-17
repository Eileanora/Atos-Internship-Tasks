using Domain.Models;
using Shared.ResourceParameters;

namespace Domain.Interfaces;

public interface IPokemonRepository : IBaseRepository<Pokemon>
{
    Task<PagedList<Pokemon>> GetAllAsync(PokemonResourceParameters resourceParameters);
    Task<bool> ExistsAsync(int id);
    Task<Pokemon?> GetByIdAsync(int id, bool includeCategories);
    Task<Pokemon?> GetByNameAsync(string name);
    Task<Decimal> GetPokemonRatingAsync(int id);
    Task <bool> CheckNameUniqueAsync(string name);
}
