using Domain.Interfaces;
using Domain.Models;
using Service.DTOs;
using Service.Interfaces;
using Service.Mappers;

namespace Service.Services;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await unitOfWork.CategoryRepository.GetAllAsync();
        return categories.Select(c => c.ToListDto());
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
        if (category == null)
            return null;
        return category.ToDetailDto();
    }

    public Task<CategoryDto> AddAsync(CategoryDto category)
    {
        throw new NotImplementedException();
    }

    public Category UpdateAsync(CategoryDto category)
    {
        throw new NotImplementedException();
    }

    public bool DeleteAsync(CategoryDto category)
    {
        throw new NotImplementedException();
    }
}