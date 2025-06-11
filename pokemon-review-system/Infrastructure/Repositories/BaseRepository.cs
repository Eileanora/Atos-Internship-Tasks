using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.Interfaces;

namespace Infrastructure.Repositories;

public class BaseRepository<TEntity>(DataContext context) : ReadOnlyBaseRepository<TEntity>(context), IBaseRepository<TEntity>
    where TEntity : BaseEntity
{    
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public TEntity UpdateAsync(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
        return entity;
    }

    // TODO: ASK WHETHER I SHOULD PASS ID OR ENTITY
    public void DeleteAsync(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }
    
    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

}
