using Domain.Models;
using Service.DTOs;

namespace Service.Mappers;

public static class PokemonMapper
{
    public static PokemonDto ToListDto(this Pokemon pokemon)
    {
        return new PokemonDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            BirthDate = pokemon.BirthDate
        };
    }
    
    public static PokemonDto ToDetailDto(this Pokemon pokemon)
    {
        return new PokemonDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            BirthDate = pokemon.BirthDate
        };
    }
    
    public static Pokemon ToUpdateEntity(this PokemonDto pokemonDto)
    {
        return new Pokemon
        {
            Name = pokemonDto.Name ?? string.Empty,
        };
    }
    
    public static Pokemon ToEntity(this PokemonDto pokemonDto)
    {
        return new Pokemon
        {
            Name = pokemonDto.Name ?? string.Empty,
            BirthDate = (DateTime)pokemonDto.BirthDate
        };
    }
}
