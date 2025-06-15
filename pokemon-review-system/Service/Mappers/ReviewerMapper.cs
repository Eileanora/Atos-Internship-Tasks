using Domain.Models;
using Shared.DTOs;

namespace Service.Mappers;

public static class ReviewerMapper
{
    public static ReviewerDto ToDto(this Reviewer reviewer)
    {
        return new ReviewerDto
        {
            Id = reviewer.Id,
            FirstName = reviewer.FirstName,
            LastName = reviewer.LastName,
            ReviewCount = reviewer.Reviews?.Count
        };
    }

    public static ReviewerDto ToListDto(this Reviewer reviewer)
    {
        return new ReviewerDto
        {
            Id = reviewer.Id,
            FirstName = reviewer.FirstName,
            LastName = reviewer.LastName,
        };
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
            Id = review.Id.ToString(),
            Title = review.Title,
            Content = review.Content,
            Rating = review.Rating,
            ReviewerId = review.ReviewerId,
            PokemonId = review.PokemonId
        };
    }
}
