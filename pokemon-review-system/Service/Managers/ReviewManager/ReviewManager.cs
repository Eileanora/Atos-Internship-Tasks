using FluentValidation;
using Service.Common.Constants;
using Service.Interfaces;
using Service.Mappers;
using Shared.ErrorAndResults;
using Shared.DTOs;
using Shared.ResourceParameters;

namespace Service.Managers.ReviewManager;

public class ReviewManager(
    IUnitOfWork unitOfWork,
    IValidator<ReviewDto> reviewValidator)
    : IReviewManager
{
    public async Task<Result<ReviewDto>> GetByIdAsync(int id)
    {
        var review = await unitOfWork.ReviewRepository.GetByIdAsyncWithIncludes(id);
        if (review == null)
            return Result<ReviewDto>.Failure(ErrorMessages.NotFound);
        return Result<ReviewDto>.Success(review.ToDto());
    }

    public async Task<Result<ReviewDto>> AddAsync(ReviewDto reviewDto)
    {
        var validationResult = await reviewValidator.ValidateAsync(reviewDto, options => options.IncludeRuleSets("CreateBusiness"));
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result<ReviewDto>.Failure(new Error("ValidationError", errorMessage));
        }
        var newReview = reviewDto.ToEntity();
        await unitOfWork.ReviewRepository.AddAsync(newReview);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<ReviewDto>.Failure(ErrorMessages.InternalServerError);
        return Result<ReviewDto>.Success(newReview.ToCreatedDto());
    }

    public async Task<Result> DeleteAsync(ReviewDto reviewDto)
    {
        var reviewEntity = await unitOfWork.ReviewRepository.FindByIdAsync((int)reviewDto.Id);
        if (reviewEntity == null)
            return Result.Failure(ErrorMessages.NotFound);
        
        unitOfWork.ReviewRepository.DeleteAsync(reviewEntity);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result.Failure(ErrorMessages.InternalServerError);
        return Result.Success();
    }

    public async Task<Result<PagedList<ReviewDto>>> GetAllAsync(ReviewResourceParameters resourceParameters)
    {
        if (resourceParameters.PokemonId == null && resourceParameters.ReviewerId == null)
            return Result<PagedList<ReviewDto>>.Success(new PagedList<ReviewDto>(new List<ReviewDto>(), 0, 0, 0));
        var pagedReviews = await unitOfWork.ReviewRepository.GetAllAsync(resourceParameters);
        return Result<PagedList<ReviewDto>>.Success(pagedReviews.ToListDto());
    }
}
