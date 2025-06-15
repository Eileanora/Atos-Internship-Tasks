using Domain.Models;
using Shared.DTOs;
using Service.Interfaces;

namespace Service.Managers.CategoryManager;

public interface ICategoryManager
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(int id);
    Task<CategoryDto> AddAsync(CategoryDto category);
    Category UpdateAsync(CategoryDto category);
    bool DeleteAsync(CategoryDto category);
}
