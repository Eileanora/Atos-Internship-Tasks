using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Infrastructure.Repositories;
using Domain.Models;

public class ReviewRepository(DataContext context) : BaseRepository<Review>(context), IReviewRepository
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
}
