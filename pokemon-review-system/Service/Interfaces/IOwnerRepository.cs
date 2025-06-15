using Domain.Models;

namespace Service.Interfaces;

public interface IOwnerRepository : IBaseRepository<Owner>
{
    Task <bool> ExistsAsync(int ownerId);
}