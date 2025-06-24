using Service.DTOs;
using Service.Interfaces;
using Shared.ErrorAndResults;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.ReviewerEndpoints;

public class DeleteReviewerEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Reviewers/{id}", async (
            IReviewerService reviewerService,
            int id) =>
        {
            var reviewerResult = await reviewerService.GetByIdAsync(id);
            if (!reviewerResult.IsSuccess)
                return reviewerResult.ToResults();
            var result = await reviewerService.DeleteAsync(reviewerResult.Value);
            if (!result.IsSuccess)
                return result.ToResults();
            return Results.NoContent();
        })
        .WithName("DeleteReviewerMinimal")
        .WithTags(Tags.Reviewer)
        .Produces<Error>(StatusCodes.Status404NotFound)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}
