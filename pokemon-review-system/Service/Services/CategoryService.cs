using Domain.Interfaces;
using Domain.Models;
using Service.Interfaces;
using Service.Mappers;
using Shared.DTOs;

namespace Service.Services;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryManager
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