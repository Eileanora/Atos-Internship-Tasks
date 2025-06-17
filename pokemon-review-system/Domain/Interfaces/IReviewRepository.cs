using Domain.Models;
using Shared.ResourceParameters;

namespace Domain.Interfaces;

public interface IReviewRepository : IBaseRepository<Review>
{
    Task<bool> ExistsAsync(int reviewerId, int pokemonId);
    Task<Review?> GetByIdAsyncWithIncludes(int id);
    Task<PagedList<Review>> GetAllAsync(
        ReviewResourceParameters resourceParameters);
}
