using Shared.DTOs;
using Shared.ErrorAndResults;
using Shared.ResourceParameters;

namespace Service.Interfaces;

public interface IOwnerManager
{
    Task<Result<PagedList<OwnerDto>>> GetAllAsync(OwnerResourceParameters resourceParameters);
    Task<Result<OwnerDto>> GetByIdAsync(int id);
    Task<Result<int>> AddAsync(CreateOwnerDto owner);
    Task<Result<OwnerDto>> UpdateAsync(OwnerDto owner);
    Task<Result> DeleteAsync(OwnerDto owner);
    Task<Result> AddPokemonToOwnerAsync(int ownerId, int pokemonId);
    Task<Result> RemovePokemonFromOwnerAsync(int ownerId, int pokemonId);
    Task<Result> ValidateOwnerAuthentication(int ownerId);
}
