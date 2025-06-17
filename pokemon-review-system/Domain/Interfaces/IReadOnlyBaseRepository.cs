using Domain.Models;

namespace Domain.Interfaces;

public interface IReadOnlyBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
}