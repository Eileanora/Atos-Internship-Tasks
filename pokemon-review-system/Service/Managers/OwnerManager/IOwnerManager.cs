using Service.Common.ErrorAndResults;
using Shared.DTOs;

namespace Service.Managers.OwnerManager;

public interface IOwnerManager
{
    Task<Result<IEnumerable<OwnerDto>>> GetAllAsync();
    Task<Result<OwnerDto>> GetByIdAsync(int id);
    Task<Result<OwnerDto>> AddAsync(OwnerDto owner);
    Task<Result<OwnerDto>> UpdateAsync(OwnerDto owner);
    Task<Result> DeleteAsync(OwnerDto owner);
    Task<Result> AddPokemonToOwnerAsync(int ownerId, int pokemonId);
    Task<Result> RemovePokemonFromOwnerAsync(int ownerId, int pokemonId);
}
