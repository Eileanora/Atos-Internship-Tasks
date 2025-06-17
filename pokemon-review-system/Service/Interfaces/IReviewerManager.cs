using Shared.DTOs;
using Shared.ErrorAndResults;
using Shared.ResourceParameters;

namespace Service.Interfaces;

public interface IReviewerManager
{
    Task<Result<PagedList<ReviewerDto>>> GetAllAsync(ReviewerResourceParameters resourceParameters);
    Task<Result<ReviewerDto>> GetByIdAsync(int id);
    Task<Result<ReviewerDto>> AddAsync(ReviewerDto reviewerDto);
    Task<Result<ReviewerDto>> UpdateAsync(ReviewerDto reviewerDto);
    Task<Result> DeleteAsync(ReviewerDto reviewerDto);
    Task<Result<IEnumerable<ReviewDto>>> GetReviewsByReviewerIdAsync(int reviewerId);
}
