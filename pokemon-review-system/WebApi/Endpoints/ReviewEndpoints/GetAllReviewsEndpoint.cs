using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Shared.ResourceParameters;
using WebApi.Helpers.PaginationHelper;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.ReviewEndpoints;

public class GetAllReviewsEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/Reviews", async (
                [AsParameters] ReviewResourceParameters resourceParameters,
                IReviewService reviewService,
                IPaginationHelper<ReviewDto, ReviewResourceParameters> paginationHelper,
                IHttpContextAccessor context,
                [FromServices] IUrlHelper urlHelper) =>
        {
            var reviews = await reviewService.GetAllAsync(resourceParameters);
            paginationHelper.CreateMetaDataHeader(
                reviews.Value, resourceParameters,
                context.HttpContext.Response.Headers,
                urlHelper,
                "GetAllReviewsMinimal");
            return reviews.ToResults();
        })
        .WithName("GetAllReviewsMinimal")
        .WithTags(Tags.Review)
        .Produces<IEnumerable<ReviewDto>>(StatusCodes.Status200OK);
    }
}
