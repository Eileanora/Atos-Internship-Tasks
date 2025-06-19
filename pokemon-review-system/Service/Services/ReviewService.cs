using Domain.Interfaces;
using FluentValidation;
using Service.Common.Constants;
using Service.DTOs;
using Service.Interfaces;
using Service.Mappers;
using Shared.ErrorAndResults;
using Shared.Helpers;
using Shared.ResourceParameters;

namespace Service.Services;

public class ReviewService(
    IUnitOfWork unitOfWork,
    IValidator<ReviewDto> reviewValidator)
    : IReviewService
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
        var validationResult = await ValidationHelper.ValidateAndReportAsync(reviewValidator, reviewDto, "CreateBusiness");
        if (!validationResult.IsSuccess)
        {
            return Result<ReviewDto>.Failure(validationResult.Error);
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
