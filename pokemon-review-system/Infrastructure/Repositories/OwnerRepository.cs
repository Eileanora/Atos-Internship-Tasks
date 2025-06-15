using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Infrastructure.Repositories;

public class OwnerRepository(DataContext context) : BaseRepository<Owner>(context), IOwnerRepository
{
    // override get by id
    public async Task<Owner?> GetByIdAsyncWithInclude(int id)
    {
        return await context.Owners
            .Include(o => o.Country)
            .Include(o => o.PokemonOwners)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
    
    public async Task<bool> ExistsAsync(int ownerId)
    {
        return await context.Owners.AnyAsync(o => o.Id == ownerId);
    }
}
