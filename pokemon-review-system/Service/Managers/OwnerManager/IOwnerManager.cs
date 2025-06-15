using Service.Common.ErrorAndResults;
using Shared.DTOs;
using Shared.ResourceParameters;

namespace Service.Managers.OwnerManager;

public interface IOwnerManager
{
    Task<Result<PagedList<OwnerDto>>> GetAllAsync(OwnerResourceParameters resourceParameters);
    Task<Result<OwnerDto>> GetByIdAsync(int id);
    Task<Result<OwnerDto>> AddAsync(OwnerDto owner);
    Task<Result<OwnerDto>> UpdateAsync(OwnerDto owner);
    Task<Result> DeleteAsync(OwnerDto owner);
    Task<Result> AddPokemonToOwnerAsync(int ownerId, int pokemonId);
    Task<Result> RemovePokemonFromOwnerAsync(int ownerId, int pokemonId);
}
