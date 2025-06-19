using Domain.Models;
using Service.DTOs;

namespace Service.Mappers;

public static class CategoryMapper
{
    public static CategoryDto ToListDto(this Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public static Category ToEntity(this CategoryDto categoryDto)
    {
        return new Category
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name
        };
    }
    
    public static CategoryDto ToDetailDto(this Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}
