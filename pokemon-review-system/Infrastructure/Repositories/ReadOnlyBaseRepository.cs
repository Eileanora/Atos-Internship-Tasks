using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Infrastructure.Repositories;

public class ReadOnlyBaseRepository<TEntity>(DataContext context): IReadOnlyBaseRepository<TEntity> 
    where TEntity : BaseEntity
{
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

}