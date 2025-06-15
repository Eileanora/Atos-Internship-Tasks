using Domain.Models;
using Service.Common.ResourceParameters;

namespace Service.Interfaces;

public interface IBaseRepository<TEntity> : IReadOnlyBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    TEntity UpdateAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
    // Task<IPagedList<TEntity>> GetAllAsync(BaseResourceParameters resourceParameters);
}
