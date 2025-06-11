using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Infrastructure.Repositories;

internal class PokemonRepository(DataContext context) : BaseRepository<Pokemon>(context), IPokemonRepository 
{
    public async Task<Pokemon?> GetByNameAsync(string name)
    {
        return await context.Pokemons
            .FirstOrDefaultAsync(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
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
            .AnyAsync(p => p.Name.ToLower() == name.ToLower());
    }

    public async Task<Pokemon?> GetByIdAsync(int id)
    {
        return await context.Pokemons
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Pokemons
            .AnyAsync(p => p.Id == id);
    }
}
