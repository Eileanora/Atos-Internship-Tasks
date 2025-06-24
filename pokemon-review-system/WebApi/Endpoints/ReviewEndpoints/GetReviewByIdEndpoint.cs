using Service.DTOs;
using Service.Interfaces;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.ReviewEndpoints;

public class GetReviewByIdEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/Reviews/{id}", async (
                int id,
                IReviewService reviewService) =>
        {
            var result = await reviewService.GetByIdAsync(id);
            return result.ToResults();
        })
        .WithName("GetReviewByIdMinimal")
        .WithTags(Tags.Review)
        .Produces<ReviewDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
