using Domain.Models;
using Service.DTOs;

namespace Service.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(int id);
    Task<CategoryDto> AddAsync(CategoryDto category);
    Category UpdateAsync(CategoryDto category);
    bool DeleteAsync(CategoryDto category);
}
