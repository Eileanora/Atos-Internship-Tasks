using Domain.Models;

namespace Service.Interfaces;

public interface ICategoryRepository : IReadOnlyBaseRepository<Category>
{
    Task<bool> ExistsAsync(int id);
    Task<Category?> GetByIdAsync(int id);
    Task <bool> CheckCategoriesExistAsync(IEnumerable<int> categoryIds);
}
