using Shared.ErrorAndResults;
using Shared.DTOs;
using Shared.ResourceParameters;

namespace Service.Managers.PokemonManager;

public interface IPokemonManager
{
    Task<Result<PagedList<PokemonDto>>> GetAllAsync(PokemonResourceParameters resourceParameters);
    Task<Result<PokemonDto>> GetByIdAsync(int id);
    Task<Result<PokemonDto>> AddAsync(PokemonDto pokemon);
    Task<Result<PokemonDto>> UpdateAsync(PokemonDto pokemon);
    Task<Result> DeleteAsync(PokemonDto pokemon);
    Task<Result<decimal>> GetPokemonRatingAsync(int pokemonId);
    
}
