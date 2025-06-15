using Domain.Models;
using Shared.DTOs;

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
            BirthDate = pokemon.BirthDate,
            Categories = pokemon.PokemonCategories?.Where(pc => pc.Category != null).Select(pc => new CategoryDto
            {
                Id = pc.Category.Id,
                Name = pc.Category.Name
            }).ToList(),
            CategoriesId = pokemon.PokemonCategories?.Where(pc => pc.Category != null).Select(pc => pc.Category.Id).ToList()
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
        // Update Categories if CategoriesId is provided
        if (pokemonDto.CategoriesId != null)
        {
            pokemon.PokemonCategories = pokemonDto.CategoriesId
                .Select(id => new PokemonCategory { CategoryId = id, PokemonId = pokemon.Id })
                .ToList();
        }
    }
    
    public static PokemonDto ToUpdateDto(this PokemonDto pokemonDto)
    {
        return new PokemonDto
        {
            Name = pokemonDto.Name ?? string.Empty,
            BirthDate = (DateTime)pokemonDto.BirthDate,
            CategoriesId = pokemonDto.CategoriesId
        };
    }
    
    public static Pokemon ToEntity(this PokemonDto pokemonDto)
    {
        var pokemon = new Pokemon
        {
            Name = pokemonDto.Name ?? string.Empty,
            BirthDate = (DateTime)pokemonDto.BirthDate,
            // Map CategoriesId to PokemonCategories if provided
            PokemonCategories = pokemonDto.CategoriesId != null
                ? pokemonDto.CategoriesId.Select(id => new PokemonCategory { CategoryId = id }).ToList()
                : new List<PokemonCategory>()
        };
        return pokemon;
    }
    
    public static PokemonDto ToCreatedDto(this Pokemon pokemon, IEnumerable<int> categoryIds)
    {
        return new PokemonDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            BirthDate = pokemon.BirthDate,
            CategoriesId = categoryIds
        };
    }
}
