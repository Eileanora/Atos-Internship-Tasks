using System.Reflection.Metadata;
using Domain.Models;
using Service.DTOs;
using Service.Helpers.Constants;
using Service.Interfaces;
using Service.Mappers;

namespace Service.Managers.PokemonManager;

public class PokemonManager(IPokemonRepository pokemonRepository) : IPokemonManager
{
    public async Task<IEnumerable<PokemonDto>> GetAllAsync()
    {
        var pokemons = await pokemonRepository.GetAllAsync();
        return pokemons.Select(p => p.ToListDto());
    }

    public async Task<PokemonDto?> GetByIdAsync(int id)
    {
        var pokemon = await pokemonRepository.GetByIdAsync(id);
        if (pokemon == null)
            return null;
        return pokemon.ToDetailDto();
    }

    public async Task<(PokemonDto?, string)> AddAsync(PokemonDto pokemon)
    {
        var nameExists = await pokemonRepository.CheckNameUniqueAsync(pokemon.Name);
        if (nameExists)
            return (null, "Pokemon name already exists");

        var newPokemon = pokemon.ToEntity();
        await pokemonRepository.AddAsync(newPokemon);

        var result = await pokemonRepository.SaveChangesAsync();
        if (!result)
            return (null, ErrorMessages.InternalServerError);

        return (newPokemon.ToDetailDto(), string.Empty);
    }

    public async Task<(PokemonDto?, string)> UpdateAsync(PokemonDto pokemon)
    {
        var pokemonExists = await pokemonRepository.GetByIdAsync((int)pokemon.Id);
        
        if (pokemonExists == null)
            return (null, ErrorMessages.NotFound);
        
        var nameExists = await pokemonRepository.CheckNameUniqueAsync(pokemon.Name);
        if (nameExists)
            return (null, "Pokemon name already exists");

        var updatedPokemon = pokemon.ToUpdateEntity();
        pokemonRepository.UpdateAsync(updatedPokemon);

        var result = await pokemonRepository.SaveChangesAsync();
        if (!result)
            return (null, ErrorMessages.InternalServerError);

        return (updatedPokemon.ToDetailDto(), string.Empty);
    }

    public async Task<bool> DeleteAsync(PokemonDto pokemon)
    {
        var pokemonExists = pokemonRepository.GetByIdAsync((int)pokemon.Id).Result;
        if (pokemonExists == null)
            return false;

        pokemonRepository.DeleteAsync(pokemonExists);
        return await pokemonRepository.SaveChangesAsync();
    }

    public async Task<decimal> GetPokemonRatingAsync(int pokemonId)
    {
        var pokemonExists = await pokemonRepository.ExistsAsync(pokemonId);
        if (!pokemonExists)
            return -500;
        
        return await pokemonRepository.GetPokemonRatingAsync(pokemonId);
    }
}
