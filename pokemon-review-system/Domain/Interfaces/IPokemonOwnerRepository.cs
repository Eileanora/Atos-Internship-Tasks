using Domain.Models;

namespace Domain.Interfaces;

public interface IPokemonOwnerRepository : IBaseRepository<PokemonOwner>
{
    Task<bool> OwnerPokemonExistsAsync(int ownerId, int pokemonId);
}