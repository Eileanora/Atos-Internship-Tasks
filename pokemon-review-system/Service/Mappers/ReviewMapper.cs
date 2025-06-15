using Domain.Models;
using Shared.DTOs;

namespace Service.Mappers;

public static class ReviewMapper
{
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
