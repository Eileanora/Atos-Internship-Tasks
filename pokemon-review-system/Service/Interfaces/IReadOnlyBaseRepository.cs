using Domain.Models;

namespace Service.Interfaces;

public interface IReadOnlyBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
}