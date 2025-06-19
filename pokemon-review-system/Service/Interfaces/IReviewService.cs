using Service.DTOs;
using Shared.ErrorAndResults;
using Shared.ResourceParameters;

namespace Service.Interfaces;

public interface IReviewService
{
    Task<Result<ReviewDto>> GetByIdAsync(int id);
    Task<Result<ReviewDto>> AddAsync(ReviewDto reviewDto);
    Task<Result> DeleteAsync(ReviewDto reviewDto);
    Task<Result<PagedList<ReviewDto>>> GetAllAsync(ReviewResourceParameters resourceParameters);
}
