using Domain.Models;
using Shared.ResourceParameters;

namespace Service.Interfaces;

public interface IReviewerRepository : IBaseRepository<Reviewer>
{
    Task<Reviewer?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> NameExistsAsync(string firstName, string lastName);
    Task<Reviewer?> GetByNameAsync(string firstName, string lastName);
    Task<Reviewer?> GetByIdAsyncWithIncludes(int id);
    Task<PagedList<Reviewer>> GetAllAsync(ReviewerResourceParameters resourceParameters);

}
