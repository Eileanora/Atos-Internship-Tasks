using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Shared.ResourceParameters;
using Infrastructure.Helpers;

namespace Infrastructure.Repositories;

public class ReviewerRepository(
    DataContext context,
    ISortHelper<Reviewer> sortHelper)
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

    public async Task<PagedList<Reviewer>> GetAllAsync(ReviewerResourceParameters resourceParameters)
    {
        var collection = context.Reviewers.AsQueryable();
        if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
        {
            var searchQuery = resourceParameters.SearchQuery.Trim().ToLower();
            collection = collection.Where(r => r.FirstName.ToLower().Contains(searchQuery) 
                                               || r.LastName.ToLower().Contains(searchQuery));
        }
        if (!string.IsNullOrWhiteSpace(resourceParameters.FirstName))
        {
            var firstName = resourceParameters.FirstName.Trim().ToLower();
            collection = collection.Where(r => r.FirstName.ToLower().Equals(firstName));
        }
        if (!string.IsNullOrWhiteSpace(resourceParameters.LastName))
        {
            var lastName = resourceParameters.LastName.Trim().ToLower();
            collection = collection.Where(r => r.LastName.ToLower().Equals(lastName));
        }
        var sortedList = sortHelper.ApplySort(collection, resourceParameters.OrderBy);
        return await CreateAsync(
            sortedList,
            resourceParameters.PageNumber,
            resourceParameters.PageSize);
    }
}
