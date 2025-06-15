using Domain.Models;
using Shared.DTOs;
using Shared.ResourceParameters;

namespace Service.Mappers;

public static class ReviewMapper
{
    private static ReviewDto ToListDto(this Review review)
    {
        var newReview = new ReviewDto
        {
            Id = review.Id,
            Title = review.Title,
            Content = review.Content,
            Rating = review.Rating,
            CreatedAt = review.CreatedDate
        };
        if (review.Reviewer != null)
        {
            newReview.ReviewerId = review.ReviewerId;
            newReview.ReviewerName = review.Reviewer.FirstName + " " + review.Reviewer.LastName;
        }
        if (review.Pokemon != null)
        {
            newReview.PokemonId = review.PokemonId;
            newReview.PokemonName = review.Pokemon.Name;
        }
        return newReview;
    }
    
    public static PagedList<ReviewDto> ToListDto(this PagedList<Review> reviews)
    {
        var count = reviews.TotalCount;
        var pageNumber = reviews.CurrentPage;
        var pageSize = reviews.PageSize;
        var totalPages = reviews.TotalPages;
        return new PagedList<ReviewDto>(
            reviews.Select(s => ToListDto(s)).ToList(),
            count,
            pageNumber,
            pageSize,
            totalPages
        );
    }
    
    public static ReviewDto ToDto(this Review review)
    {
        return new ReviewDto
        {
            Id = review.Id,
            Title = review.Title,
            Content = review.Content,
            Rating = review.Rating,
            ReviewerId = review.ReviewerId,
            ReviewerName = review.Reviewer.FirstName + " " + review.Reviewer.LastName,
            PokemonId = review.PokemonId,
            PokemonName = review.Pokemon.Name,
            CreatedAt = review.CreatedDate
        };
    }
    public static ReviewDto ToCreatedDto(this Review review)
    {
        return new ReviewDto
        {
            Id = review.Id,
            Title = review.Title,
            Content = review.Content,
            Rating = review.Rating,
            ReviewerId = review.ReviewerId,
            PokemonId = review.PokemonId,
            CreatedAt = review.CreatedDate
        };
    }
    public static Review ToEntity(this ReviewDto dto)
    {
        return new Review
        {
            Title = dto.Title ?? string.Empty,
            Content = dto.Content ?? string.Empty,
            Rating = dto.Rating ?? 0,
            ReviewerId = dto.ReviewerId ?? 0,
            PokemonId = dto.PokemonId ?? 0
        };
    }
}
