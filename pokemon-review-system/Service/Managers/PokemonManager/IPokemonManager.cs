using Domain.Models;
using Service.DTOs;

namespace Service.Managers.PokemonManager;

public interface IPokemonManager
{
    Task<IEnumerable<PokemonDto>> GetAllAsync();
    Task<PokemonDto?> GetByIdAsync(int id);
    Task<(PokemonDto?, string)> AddAsync(PokemonDto pokemon);
    Task<(PokemonDto?, string)> UpdateAsync(PokemonDto pokemon);
    Task<bool> DeleteAsync(PokemonDto pokemon);
    Task<Decimal> GetPokemonRatingAsync(int pokemonId);
    
}
