using Domain.Models;
using Service.Common.ErrorAndResults;
using Service.DTOs;

namespace Service.Managers.PokemonManager;

public interface IPokemonManager
{
    Task<Result<IEnumerable<PokemonDto>>> GetAllAsync();
    Task<Result<PokemonDto>> GetByIdAsync(int id);
    Task<Result<PokemonDto>> AddAsync(PokemonDto pokemon);
    Task<Result<PokemonDto>> UpdateAsync(PokemonDto pokemon);
    Task<Result> DeleteAsync(PokemonDto pokemon);
    Task<Result<decimal>> GetPokemonRatingAsync(int pokemonId);
    
}
