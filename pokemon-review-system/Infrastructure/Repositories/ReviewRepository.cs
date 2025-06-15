using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Shared.ResourceParameters;
using Infrastructure.Helpers;

namespace Infrastructure.Repositories;
using Domain.Models;

public class ReviewRepository(
    DataContext context,
    ISortHelper<Review> sortHelper) : BaseRepository<Review>(context), IReviewRepository
{
    public async Task<bool> ExistsAsync(int reviewerId, int pokemonId)
    {
        return await context.Reviews.AnyAsync(r => r.ReviewerId == reviewerId && r.PokemonId == pokemonId);
    }

    public async Task<Review?> GetByIdAsyncWithIncludes(int id)
    {
        return await context.Reviews
            .Include(r => r.Pokemon)
            .Include(r => r.Reviewer)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<PagedList<Review>> GetAllAsync(
        ReviewResourceParameters resourceParameters)
    {
        var collection = context.Reviews.AsQueryable().AsNoTracking();
        if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
        {
            var searchQuery = resourceParameters.SearchQuery.Trim().ToLower();
            collection = collection.Where(r => r.Title.ToLower().Contains(searchQuery) || r.Content.ToLower().Contains(searchQuery));
        }
        
        if (resourceParameters.PokemonId.HasValue)
        {
            collection = collection.Where(r => r.PokemonId == resourceParameters.PokemonId)
                .Include(r => r.Reviewer);
        }
        
        if (resourceParameters.ReviewerId.HasValue)
        {
            collection = collection.Where(r => r.ReviewerId == resourceParameters.ReviewerId)
                .Include(r => r.Pokemon);
        }
        
        if (resourceParameters.Rating.HasValue)
        {
            collection = collection.Where(r => r.Rating == resourceParameters.Rating);
        }
        var sortedList = sortHelper.ApplySort(collection, resourceParameters.OrderBy);
        return await CreateAsync(
            sortedList,
            resourceParameters.PageNumber,
            resourceParameters.PageSize);
    }
}
