using Domain.Models;
using Infrastructure.Helpers;
using Service.Common.ResourceParameters;

namespace Service.Interfaces;

public interface IPokemonRepository : IBaseRepository<Pokemon>
{
    Task<PagedList<Pokemon>> GetAllAsync(PokemonResourceParameters resourceParameters);
    Task<bool> ExistsAsync(int id);
    Task<Pokemon?> GetByIdAsync(int id);
    Task<Pokemon?> GetByNameAsync(string name);
    Task<Decimal> GetPokemonRatingAsync(int id);
    Task <bool> CheckNameUniqueAsync(string name);
}
