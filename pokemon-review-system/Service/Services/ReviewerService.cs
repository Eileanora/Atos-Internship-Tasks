using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Internal;
using Service.Common.Constants;
using Service.DTOs;
using Service.Interfaces;
using Service.Mappers;
using Shared.ErrorAndResults;
using Shared.Helpers;
using Shared.ResourceParameters;

namespace Service.Services;

public class ReviewerService(
    IUnitOfWork unitOfWork,
    IValidator<ReviewerDto> reviewerValidator)
    : IReviewerService
{
    public async Task<Result<PagedList<ReviewerDto>>> GetAllAsync(ReviewerResourceParameters resourceParameters)
    {
        var reviewers = await unitOfWork.ReviewerRepository.GetAllAsync(resourceParameters);
        return Result<PagedList<ReviewerDto>>.Success(reviewers.ToListDto());
    }

    public async Task<Result<ReviewerDto>> GetByIdAsync(int id)
    {
        var reviewer = await unitOfWork.ReviewerRepository.GetByIdAsyncWithIncludes(id);
        if (reviewer == null)
            return Result<ReviewerDto>.Failure(ErrorMessages.NotFound);
        return Result<ReviewerDto>.Success(reviewer.ToDetailDto());
    }

    public async Task<Result<ReviewerDto>> AddAsync(ReviewerDto reviewerDto)
    {
        var validationResult = await ValidationHelper.ValidateAndReportAsync(reviewerValidator, reviewerDto, "CreateBusiness");
        if (!validationResult.IsSuccess)
        {
            return Result<ReviewerDto>.Failure(validationResult.Error);
        }
        var newReviewer = reviewerDto.ToEntity();
        await unitOfWork.ReviewerRepository.AddAsync(newReviewer);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<ReviewerDto>.Failure(ErrorMessages.InternalServerError);
        return Result<ReviewerDto>.Success(newReviewer.ToCreatedDto());
    }

    public async Task<Result<ReviewerDto>> UpdateAsync(ReviewerDto reviewerDto)
    {
        var validationResult = await ValidationHelper.ValidateAndReportAsync(reviewerValidator,
            reviewerDto,
            ctx => { ctx.RootContextData["reviewerId"] = reviewerDto.Id; },
            "UpdateBusiness");
        if (!validationResult.IsSuccess)
        {
            return Result<ReviewerDto>.Failure(validationResult.Error);
        }
        
        var reviewerEntity = await unitOfWork.ReviewerRepository.GetByIdAsync(reviewerDto.Id ?? 0);
        reviewerEntity.UpdateEntityFromDto(reviewerDto);
        
        unitOfWork.ReviewerRepository.UpdateAsync(reviewerEntity);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<ReviewerDto>.Failure(ErrorMessages.InternalServerError);
        return Result<ReviewerDto>.Success(reviewerDto);
    }

    public async Task<Result> DeleteAsync(ReviewerDto reviewerDto)
    {
        var reviewerEntity = await unitOfWork.ReviewerRepository.GetByIdAsync(reviewerDto.Id ?? 0);
        if (reviewerEntity == null)
            return Result.Failure(ErrorMessages.NotFound);
        
        unitOfWork.ReviewerRepository.DeleteAsync(reviewerEntity);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result.Failure(ErrorMessages.InternalServerError);
        return Result.Success();
    }

    // TODO: This is a placeholder logic, review this method after implementing the methods
    public async Task<Result<IEnumerable<ReviewDto>>> GetReviewsByReviewerIdAsync(int reviewerId)
    {
        var reviewer = await unitOfWork.ReviewerRepository.GetByIdAsync(reviewerId);
        if (reviewer == null)
            return Result<IEnumerable<ReviewDto>>.Failure(ErrorMessages.NotFound);
        var reviews = reviewer.Reviews?.Select(r => r.ToReviewDto()) ?? Enumerable.Empty<ReviewDto>();
        return Result<IEnumerable<ReviewDto>>.Success(reviews);
    }
}
