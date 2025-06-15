using Domain.Models;
using Shared.DTOs;
using Shared.ResourceParameters;

namespace Service.Mappers;

public static class ReviewerMapper
{
    private static ReviewerDto ToListDto(this Reviewer reviewer)
    {
        return new ReviewerDto
        {
            Id = reviewer.Id,
            FirstName = reviewer.FirstName,
            LastName = reviewer.LastName,
        };
    }
    
    public static PagedList<ReviewerDto> ToListDto(this PagedList<Reviewer> reviewers)
    {
        var count = reviewers.TotalCount;
        var pageNumber = reviewers.CurrentPage;
        var pageSize = reviewers.PageSize;
        var totalPages = reviewers.TotalPages;
        return new PagedList<ReviewerDto>(
            reviewers.Select(s => ToListDto(s)).ToList(),
            count,
            pageNumber,
            pageSize,
            totalPages
        );
    }

    public static ReviewerDto ToDetailDto(this Reviewer reviewer)
    {
        return new ReviewerDto
        {
            Id = reviewer.Id,
            FirstName = reviewer.FirstName,
            LastName = reviewer.LastName,
            ReviewCount = reviewer.Reviews?.Count
        };
    }

    public static ReviewerDto ToCreatedDto(this Reviewer reviewer)
    {
        return new ReviewerDto
        {
            Id = reviewer.Id,
            FirstName = reviewer.FirstName,
            LastName = reviewer.LastName
        };
    }

    public static Reviewer ToEntity(this ReviewerDto dto)
    {
        return new Reviewer
        {
            FirstName = dto.FirstName ?? string.Empty,
            LastName = dto.LastName ?? string.Empty
        };
    }

    public static void UpdateEntityFromDto(this Reviewer entity, ReviewerDto dto)
    {
        entity.FirstName = dto.FirstName ?? string.Empty;
        entity.LastName = dto.LastName ?? string.Empty;
    }

    public static ReviewDto ToReviewDto(this Review review)
    {
        return new ReviewDto
        {
            Id = review.Id,
            Title = review.Title,
            Content = review.Content,
            Rating = review.Rating,
            ReviewerId = review.ReviewerId,
            PokemonId = review.PokemonId
        };
    }
}
