using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.ResourceParameters;
using Infrastructure.Helpers;

namespace Infrastructure.Repositories;

public class OwnerRepository(DataContext context,
    ISortHelper<Owner> sortHelper)
    : BaseRepository<Owner>(context), IOwnerRepository
{
    public async Task<Owner?> GetByIdAsyncWithInclude(int id)
    {
        return await context.Owners
            .Include(o => o.Country)
            .Include(o => o.PokemonOwners)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
    
    public async Task<bool> ExistsAsync(int ownerId)
    {
        return await context.Owners
            .IgnoreQueryFilters()
            .AnyAsync(o => o.Id == ownerId);
    }

    public async Task<PagedList<Owner>> GetAllAsync(OwnerResourceParameters resourceParameters)
    {
        var collection = context.Owners.AsQueryable().AsNoTracking().IgnoreQueryFilters();
        if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
        {
            var searchQuery = resourceParameters.SearchQuery.Trim().ToLower();
            collection = collection.Where(o => o.FirstName.ToLower().Contains(searchQuery) || o.LastName.ToLower().Contains(searchQuery));
        }
        
        if (!string.IsNullOrWhiteSpace(resourceParameters.FirstName))
        {
            var firstName = resourceParameters.FirstName.Trim().ToLower();
            collection = collection.Where(o => o.FirstName.ToLower().Equals(firstName));
        }
        
        if (!string.IsNullOrWhiteSpace(resourceParameters.LastName))
        {
            var lastName = resourceParameters.LastName.Trim().ToLower();
            collection = collection.Where(o => o.LastName.ToLower().Equals(lastName));
        }
        
        if (resourceParameters.CountryId.HasValue)
        {
            collection = collection.Where(o => o.CountryId == resourceParameters.CountryId);
        }
        
        if (!string.IsNullOrWhiteSpace(resourceParameters.Gym))
        {
            var gym = resourceParameters.Gym.Trim().ToLower();
            collection = collection.Where(o => o.Gym.ToLower().Contains(gym));
        }
        
        var sortedList = sortHelper.ApplySort(collection, resourceParameters.OrderBy);
        return await CreateAsync(
            sortedList,
            resourceParameters.PageNumber,
            resourceParameters.PageSize);
    }
    
    public async Task<bool> OwnerIdIsUserIdAsync(int ownerId, string userId)
    {
        return await context.Owners
            .IgnoreQueryFilters()
            .AnyAsync(o => o.Id == ownerId && o.UserId == userId);
    }
}
