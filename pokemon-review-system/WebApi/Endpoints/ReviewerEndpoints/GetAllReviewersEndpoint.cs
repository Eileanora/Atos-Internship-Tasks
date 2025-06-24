using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Shared.ErrorAndResults;
using Shared.ResourceParameters;
using WebApi.Helpers.PaginationHelper;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.ReviewerEndpoints;

public class GetAllReviewersEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/Reviewers", async (
                [AsParameters] ReviewerResourceParameters resourceParameters,
                IReviewerService reviewerService,
                IPaginationHelper<ReviewerDto, ReviewerResourceParameters> paginationHelper,
                IHttpContextAccessor context,
                [FromServices] IUrlHelper urlHelper) =>
        {
            var reviewers = await reviewerService.GetAllAsync(resourceParameters);
            paginationHelper.CreateMetaDataHeader(
                reviewers.Value, resourceParameters,
                context.HttpContext.Response.Headers,
                urlHelper,
                "GetAllReviewersMinimal");
            return reviewers.ToResults();
        })
        .WithName("GetAllReviewersMinimal")
        .WithTags(Tags.Reviewer)
        .Produces<PagedResult<ReviewerDto>>(StatusCodes.Status200OK)
        .Produces<Error>(StatusCodes.Status500InternalServerError);
    }
}
