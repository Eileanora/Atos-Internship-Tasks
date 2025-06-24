using FluentValidation;
using Service.DTOs;
using Service.Interfaces;
using Shared.Helpers;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.ReviewEndpoints;

public class AddReviewEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/Reviews", async (
                IReviewService reviewService,
                IValidator<ReviewDto> reviewValidator,
                ReviewDto reviewDto) =>
        {
            var inputValid = await ValidationHelper.ValidateAndReportAsync(reviewValidator, reviewDto, "Input");
            if (!inputValid.IsSuccess)
                return inputValid.ToResults();
            var result = await reviewService.AddAsync(reviewDto);
            if (!result.IsSuccess)
                return result.ToResults();
            return Results.CreatedAtRoute("GetReviewByIdMinimal", new { id = result.Value.Id }, result.Value);
        })
        .WithName("AddReviewMinimal")
        .WithTags(Tags.Review)
        .Produces<ReviewDto>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
