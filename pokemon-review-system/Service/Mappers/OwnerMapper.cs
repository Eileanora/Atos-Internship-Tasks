using Domain.Models;
using Shared.DTOs;

namespace Service.Mappers;

public static class OwnerMapper
{
    // ToListDto, ToDetailDto, ToUpdateDto, ToCreatedDto, ToEntity, UpdateEntityFromDto
    public static OwnerDto ToListDto(this Owner owner)
    {
        return new OwnerDto
        {
            Id = owner.Id,
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            Gym = owner.Gym
        };
    }
    
    public static OwnerDto ToDetailDto(this Owner owner)
    {
        return new OwnerDto
        {
            Id = owner.Id,
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            Gym = owner.Gym,
            CountryId = owner.CountryId,
            CountryName = owner.Country?.Name,
            PokemonIds = owner.PokemonOwners.Select(po => po.PokemonId),
            PokemonCount = owner.PokemonOwners.Count,
        };
    }

    public static OwnerDto ToUpdateDto(this OwnerDto ownerDto)
    {
        return new OwnerDto
        {
            Id = ownerDto.Id,
            FirstName = ownerDto.FirstName,
            LastName = ownerDto.LastName,
            Gym = ownerDto.Gym,
            CountryId = ownerDto.CountryId
        };
    }
    
    public static Owner ToEntity(this OwnerDto ownerDto)
    {
        return new Owner
        {
            Id = ownerDto.Id ?? 0,
            FirstName = ownerDto.FirstName ?? string.Empty,
            LastName = ownerDto.LastName ?? string.Empty,
            Gym = ownerDto.Gym ?? string.Empty,
            CountryId = ownerDto.CountryId ?? 0
        };
    }
    
    public static void UpdateEntityFromDto(this Owner owner, OwnerDto ownerDto)
    {
        owner.FirstName = ownerDto.FirstName ?? string.Empty;
        owner.LastName = ownerDto.LastName ?? string.Empty;
        owner.Gym = ownerDto.Gym ?? string.Empty;
        owner.CountryId = (int)ownerDto.CountryId;
    }
    
    public static OwnerDto ToCreatedDto(this Owner owner)
    {
        return new OwnerDto
        {
            Id = owner.Id,
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            Gym = owner.Gym,
            CountryId = owner.CountryId
        };
    }
}
