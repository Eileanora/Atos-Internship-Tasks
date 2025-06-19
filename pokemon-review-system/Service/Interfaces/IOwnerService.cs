using Service.DTOs;
using Shared.ErrorAndResults;
using Shared.ResourceParameters;

namespace Service.Interfaces;

public interface IOwnerService
{
    Task<Result<PagedList<OwnerDto>>> GetAllAsync(OwnerResourceParameters resourceParameters);
    Task<Result<OwnerDto>> GetByIdAsync(string id);
    Task<Result<OwnerDto>> AddAsync(CreateOwnerDto owner);
    Task<Result<OwnerDto>> UpdateAsync(OwnerDto owner);
    Task<Result> DeleteAsync(OwnerDto owner);
    Task<Result> AddPokemonToOwnerAsync(string ownerId, int pokemonId);
    Task<Result> RemovePokemonFromOwnerAsync(string ownerId, int pokemonId);
    Task<Result<OwnerDto>> GetByIdForPatchAsync(string id);
    Task<Result> ValidateOwnerAuthentication(string ownerId);
}
