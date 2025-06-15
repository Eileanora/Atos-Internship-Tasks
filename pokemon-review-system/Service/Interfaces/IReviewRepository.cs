using Domain.Models;

namespace Service.Interfaces;

public interface IReviewRepository : IBaseRepository<Review>
{
    Task<bool> ExistsAsync(int reviewerId, int pokemonId);
    Task<Review?> GetByIdAsyncWithIncludes(int id);
}
