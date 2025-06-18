using Domain.Models;
using Shared.ResourceParameters;

namespace Domain.Interfaces;

public interface IOwnerRepository : IBaseRepository<Owner>
{
    Task <bool> ExistsAsync(int ownerId);
    Task<Owner?> GetByIdAsyncWithInclude(int id);
    Task<PagedList<Owner>> GetAllAsync(OwnerResourceParameters resourceParameters);
    Task<bool> OwnerIdIsUserIdAsync(int ownerId, string userId);
}
