using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Shared.ResourceParameters;

namespace Infrastructure.Repositories;

internal class PokemonRepository(
    DataContext context,
    ISortHelper<Pokemon> sortHelper) : BaseRepository<Pokemon>(context), IPokemonRepository 
{
    public async Task<Pokemon?> GetByNameAsync(string name)
    {
        return await context.Pokemons
            .FirstOrDefaultAsync(p => p.Name.Equals(name.ToLower()));
    }

    public async Task<decimal> GetPokemonRatingAsync(int id)
    {
        var ratings = await context.Reviews
            .Where(r => r.PokemonId == id)
            .Select(r => (decimal)r.Rating)
            .ToListAsync();
        if (ratings.Count == 0)
            return 0;
        return ratings.Average();
    }

    public async Task<bool> CheckNameUniqueAsync(string name)
    {
        return !await context.Pokemons
            .AsNoTracking()
            .AnyAsync(p => p.Name.ToLower() == name.ToLower());
    }

    public async Task<Pokemon?> GetByIdAsync(int id, bool includeCategories = false)
    {
        if (includeCategories)
        {
            return await context.Pokemons
                .Include(p => p.PokemonCategories)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        return await context.Pokemons
            .Include(p => p.PokemonCategories)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PagedList<Pokemon>> GetAllAsync(PokemonResourceParameters resourceParameters)
    {
        var collection = context.Pokemons.AsQueryable().AsNoTracking();
        if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
        {
            var searchQuery = resourceParameters.SearchQuery.Trim().ToLower();
            collection = collection.Where(p => p.Name.Contains(searchQuery));
        } 
        
        if (!string.IsNullOrWhiteSpace(resourceParameters.Name))
        {
            var pokemonName = resourceParameters.Name.Trim().ToLower();
            collection = collection.Where(p => p.Name.Equals(pokemonName));
        }
        
        var sortedList = sortHelper.ApplySort(collection, resourceParameters.OrderBy);
        
        return await CreateAsync(
            sortedList,
            resourceParameters.PageNumber,
            resourceParameters.PageSize);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Pokemons
            .AnyAsync(p => p.Id == id);
    }
}
