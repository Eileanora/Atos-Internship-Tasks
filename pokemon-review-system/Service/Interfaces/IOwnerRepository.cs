using Domain.Models;
using Shared.ResourceParameters;

namespace Service.Interfaces;

public interface IOwnerRepository : IBaseRepository<Owner>
{
    Task <bool> ExistsAsync(int ownerId);
    Task<Owner?> GetByIdAsyncWithInclude(int id);
    Task<PagedList<Owner>> GetAllAsync(OwnerResourceParameters resourceParameters);
}
