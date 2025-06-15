using Service.Common.ErrorAndResults;
using Shared.DTOs;

namespace Service.Managers.ReviewerManager;

public interface IReviewerManager
{
    Task<Result<IEnumerable<ReviewerDto>>> GetAllAsync();
    Task<Result<ReviewerDto>> GetByIdAsync(int id);
    Task<Result<ReviewerDto>> AddAsync(ReviewerDto reviewerDto);
    Task<Result<ReviewerDto>> UpdateAsync(ReviewerDto reviewerDto);
    Task<Result> DeleteAsync(ReviewerDto reviewerDto);
    Task<Result<IEnumerable<ReviewDto>>> GetReviewsByReviewerIdAsync(int reviewerId);
}
