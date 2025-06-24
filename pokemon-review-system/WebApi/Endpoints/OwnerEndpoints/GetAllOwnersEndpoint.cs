using System.Linq.Dynamic.Core;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Shared.ErrorAndResults;
using Shared.Helpers;
using Shared.ResourceParameters;
using WebApi.Filters;
using WebApi.Helpers.Extensions;
using WebApi.Helpers.PaginationHelper;

namespace WebApi.Endpoints.OwnerEndpoints;

public class GetAllOwnersEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/Owners", async (
                [AsParameters] OwnerResourceParameters resourceParameters,
                [FromServices] IOwnerService ownerManager,
                [FromServices] IPaginationHelper<OwnerDto, OwnerResourceParameters> paginationHelper,
                [FromServices] IUrlHelper urlHelper,
                HttpResponse response) =>
            {
                var owners = await ownerManager.GetAllAsync(resourceParameters);
                paginationHelper.CreateMetaDataHeader(
                    owners.Value, resourceParameters, response.Headers, urlHelper, "GetAllOwnersMinimal");
                return owners.ToResults();
            })
            .WithName("GetAllOwnersMinimal")
            .WithTags(Tags.Owner)
            .RequireAuthorization("Admin")
            .Produces<PagedResult<OwnerDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);
    }
}
