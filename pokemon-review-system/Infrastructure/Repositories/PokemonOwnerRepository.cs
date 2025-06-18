using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PokemonOwnerRepository(DataContext context) : BaseRepository<PokemonOwner>(context), IPokemonOwnerRepository
{
    public async Task<bool> OwnerPokemonExistsAsync(int ownerId, int pokemonId)
    {
        return await context.PokemonOwners
            .IgnoreQueryFilters()
            .AnyAsync(po => po.OwnerId == ownerId && po.PokemonId == pokemonId);
    }
}
