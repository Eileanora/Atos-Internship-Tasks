using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Shared.ErrorAndResults;
using Shared.Helpers;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.ReviewerEndpoints;

public class AddReviewerEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/Reviewers", async (
                [FromBody] ReviewerDto reviewerDto,
                [FromServices] IReviewerService reviewerService,
                [FromServices] IValidator<ReviewerDto> reviewerValidator) =>
        {
            var inputValid = await ValidationHelper.ValidateAndReportAsync(reviewerValidator, reviewerDto, "Input");
            if (!inputValid.IsSuccess)
                return inputValid.ToResults();
            var result = await reviewerService.AddAsync(reviewerDto);
            if (!result.IsSuccess)
                return result.ToResults();
            return Results.CreatedAtRoute("GetReviewerByIdMinimal", new { id = result.Value.Id }, result.Value);
        })
        .WithName("AddReviewerMinimal")
        .WithTags(Tags.Reviewer)
        .Produces<ReviewerDto>(StatusCodes.Status201Created)
        .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}
