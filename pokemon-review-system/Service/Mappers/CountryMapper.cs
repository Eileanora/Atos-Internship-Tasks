using Service.DTOs;

namespace Service.Mappers;

public static class CountryMapper
{
    public static CountryDto ToListDto(this Domain.Models.Country country)
    {
        return new CountryDto
        {
            Id = country.Id,
            Name = country.Name
        };
    }
}