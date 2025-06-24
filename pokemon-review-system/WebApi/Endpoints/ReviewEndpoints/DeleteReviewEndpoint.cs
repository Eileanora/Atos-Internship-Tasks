using Service.Interfaces;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.ReviewEndpoints;

public class DeleteReviewEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/reviews/{id}", async (
                int id,
                IReviewService reviewService) =>
        {
            var reviewResult = await reviewService.GetByIdAsync(id);
            if (!reviewResult.IsSuccess)
                return reviewResult.ToResults();
            var result = await reviewService.DeleteAsync(reviewResult.Value);
            if (!result.IsSuccess)
                return result.ToResults();
            return Results.NoContent();
        })
        .WithName("DeleteReviewMinimal")
        .WithTags(Tags.Review)
        .Produces(204)
        .Produces(404);
    }
}

