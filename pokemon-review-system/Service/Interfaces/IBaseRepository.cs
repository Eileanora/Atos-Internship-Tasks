using Domain.Models;

namespace Service.Interfaces;

public interface IBaseRepository<TEntity> : IReadOnlyBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    TEntity UpdateAsync(TEntity entity);
    Task<bool> SaveChangesAsync();
    void DeleteAsync(TEntity entity);
}
