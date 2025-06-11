using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Infrastructure.Repositories;

public class CategoryRepository(DataContext context) : ReadOnlyBaseRepository<Category>(context), ICategoryRepository
{
    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Categories
            .AnyAsync(c => c.Id == id);
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
