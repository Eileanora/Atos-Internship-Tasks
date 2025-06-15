using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Infrastructure.Repositories;

public class ReviewerRepository(
    DataContext context)
    : BaseRepository<Reviewer>(context), IReviewerRepository
{
    public async Task<Reviewer?> GetByIdAsync(int id)
    {
        return await context.Reviewers.FindAsync(id);
    }
    
    public async Task<Reviewer?> GetByIdAsyncWithIncludes(int id)
    {
        return await context.Reviewers
            .Include(r => r.Reviews)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Reviewers.AnyAsync(r => r.Id == id);
    }

    public async Task<bool> NameExistsAsync(string firstName, string lastName)
    {
        return await context.Reviewers.AnyAsync(r => r.FirstName == firstName && r.LastName == lastName);
    }

    public async Task<Reviewer?> GetByNameAsync(string firstName, string lastName)
    {
        return await context.Reviewers
            .FirstOrDefaultAsync(r => r.FirstName == firstName && r.LastName == lastName);
    }
}
