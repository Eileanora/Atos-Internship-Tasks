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

    // public static void ToUpdateEntity(this PokemonDto pokemonDto, Pokemon pokemon)
    // {
    //     pokemon.Name = pokemonDto.Name;
    //     pokemon.BirthDate = (DateTime)pokemonDto.BirthDate;
    // }
    
    public static void UpdateEntityFromDto(this Pokemon pokemon, PokemonDto pokemonDto)
    {
        pokemon.Name = pokemonDto.Name ?? string.Empty;
        pokemon.BirthDate = (DateTime)pokemonDto.BirthDate;
    }
    
    public static PokemonDto ToUpdateDto(this PokemonDto pokemonDto)
    {
        return new PokemonDto
        {
            Name = pokemonDto.Name ?? string.Empty,
            BirthDate = (DateTime)pokemonDto.BirthDate,
        };
    }
    
    public static Pokemon ToEntity(this PokemonDto pokemonDto)
    {
        var pokemon = new Pokemon
        {
            Name = pokemonDto.Name ?? string.Empty,
            BirthDate = (DateTime)pokemonDto.BirthDate
        };
        if (pokemonDto.Id != null)
            pokemon.Id = (int)pokemonDto.Id;
        return pokemon;
    }
}
