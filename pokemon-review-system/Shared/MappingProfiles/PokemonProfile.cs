using AutoMapper;
using Domain.Models;
using Shared.DTOs;

namespace Shared.MappingProfiles;

public class PokemonProfile : Profile
{
    public PokemonProfile()
    {
        // create a mapping configuration between type a and type b
        CreateMap<Pokemon, PokemonDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src
                .PokemonCategories
                .Select(c => new CategoryDto()
                {
                    Id = c.Category.Id,
                    Name = c.Category.Name
                })))
            .ReverseMap();
    }
}
