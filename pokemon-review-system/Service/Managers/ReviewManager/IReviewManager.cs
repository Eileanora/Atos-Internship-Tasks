using Service.Common.ErrorAndResults;
using Shared.DTOs;

namespace Service.Managers.ReviewManager;

public interface IReviewManager
{
    Task<Result<ReviewDto>> GetByIdAsync(int id);
    Task<Result<ReviewDto>> AddAsync(ReviewDto reviewDto);
    Task<Result> DeleteAsync(ReviewDto reviewDto);
}
