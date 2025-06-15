using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Service.Common.ResourceParameters;
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
        context.Entry(entity).State = EntityState.Modified;
        return entity;
    }

    // TODO: ASK WHETHER I SHOULD PASS ID OR ENTITY
    public void DeleteAsync(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    protected static async Task<PagedList<TEntity>> CreateAsync(
        IQueryable<TEntity> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PagedList<TEntity>(items, count, pageNumber, pageSize);
    }
}
