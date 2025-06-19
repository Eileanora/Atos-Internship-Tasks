using Domain.Models;
using Service.DTOs;
using Shared.ResourceParameters;

namespace Service.Mappers;

public static class OwnerMapper
{
    // ToListDto, ToDetailDto, ToUpdateDto, ToCreatedDto, ToEntity, UpdateEntityFromDto
    private static OwnerDto ToListDto(this Owner owner)
    {
        return new OwnerDto
        {
            Id = owner.UserId,
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            Gym = owner.Gym
        };
    }
    
    public static PagedList<OwnerDto> ToListDto(this PagedList<Owner> owners)
    {
        var count = owners.TotalCount;
        var pageNumber = owners.CurrentPage;
        var pageSize = owners.PageSize;
        var totalPages = owners.TotalPages;
        return new PagedList<OwnerDto>(
            owners.Select(s => ToListDto(s)).ToList(),
            count,
            pageNumber,
            pageSize,
            totalPages
        );
    }
    
    public static OwnerDto ToDetailDto(this Owner owner)
    {
        return new OwnerDto
        {
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            Gym = owner.Gym,
            CountryId = owner.CountryId,
            CountryName = owner.Country?.Name,
            PokemonIds = owner.PokemonOwners.Select(po => po.PokemonId),
            PokemonCount = owner.PokemonOwners.Count,
            Id = owner.UserId
        };
    }

    public static OwnerDto ToUpdateDto(this Owner owner)
    {
        return new OwnerDto
        {
            HiddenId = owner.Id,
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            Gym = owner.Gym,
            CountryId = owner.CountryId,
            Id = owner.UserId,
        };
    }
    
    public static Owner ToEntity(this CreateOwnerDto ownerDto)
    {
        return new Owner
        {
            FirstName = ownerDto.FirstName ?? string.Empty,
            LastName = ownerDto.LastName ?? string.Empty,
            Gym = ownerDto.Gym ?? string.Empty,
            CountryId = (int)ownerDto.CountryId
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
            Id = owner.UserId,
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            Gym = owner.Gym,
            CountryId = owner.CountryId
        };
    }
}
