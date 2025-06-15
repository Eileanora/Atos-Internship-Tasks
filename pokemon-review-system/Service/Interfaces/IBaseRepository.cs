using Domain.Models;
using Service.Common.ResourceParameters;

namespace Service.Interfaces;

public interface IBaseRepository<TEntity> : IReadOnlyBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    TEntity UpdateAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
    // TODO: Violation of Interface Segregation Principle, Table with composite key doesnt need this
    Task<TEntity?> FindByIdAsync(int id);
    Task<TEntity?> GetByIdAsyncWithInclude(int id);
    // Task<IPagedList<TEntity>> GetAllAsync(BaseResourceParameters resourceParameters);
}
