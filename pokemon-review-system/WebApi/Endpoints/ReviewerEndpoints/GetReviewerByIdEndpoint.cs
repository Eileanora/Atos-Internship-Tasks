using Service.DTOs;
using Service.Interfaces;
using Shared.ErrorAndResults;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.ReviewerEndpoints;

public class GetReviewerByIdEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/Reviewers/{id}", async (IReviewerService reviewerService, int id) =>
        {
            var result = await reviewerService.GetByIdAsync(id);
            return result.ToResults();
        })
        .WithName("GetReviewerByIdMinimal")
        .WithTags(Tags.Reviewer)
        .Produces<ReviewerDto>(StatusCodes.Status200OK)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .Produces<Error>(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}
