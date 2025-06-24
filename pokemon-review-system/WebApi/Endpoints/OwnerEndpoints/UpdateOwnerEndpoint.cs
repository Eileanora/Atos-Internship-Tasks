using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Shared.Helpers;
using WebApi.Filters;

namespace WebApi.Endpoints.OwnerEndpoints;

public class UpdateOwnerEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        // app.MapPatch("minimal/api/Owners/{id}", async (
        //         [FromRoute] string id,
        //         [FromBody] JsonPatchDocument<OwnerDto> patchDoc,
        //         [FromServices] IOwnerService ownerManager,
        //         [FromServices] IValidator<OwnerDto> ownerValidator) =>
        // {
        //     var ownerResult = await ownerManager.GetByIdForPatchAsync(id);
        //     if (!ownerResult.IsSuccess)
        //         return ownerResult.ToResults();
        //     var ownerToPatch = ownerResult.Value;
        //     // Assuming you have a HandlePatch extension method as in the controller
        //     var (patchedDto, validationResult) = ownerToPatch.HandlePatch(patchDoc);
        //     if (!validationResult.IsSuccess)
        //         return validationResult.ToResults();
        //     var inputValid = await ValidationHelper.ValidateAndReportAsync(ownerValidator, patchedDto, "input");
        //     if (!inputValid.IsSuccess)
        //         return inputValid.ToResults();
        //     patchedDto.Id = id;
        //     var result = await ownerManager.UpdateAsync(patchedDto);
        //     if (!result.IsSuccess)
        //         return result.ToResults();
        //     return Results.NoContent();
        // })
        // .WithName("UpdateOwner")
        // .RequireAuthorization()
        // .AddEndpointFilter<AuthOwnerByIdFilter>()
        // .Produces(StatusCodes.Status404NotFound)
        // .Produces(StatusCodes.Status400BadRequest)
        // .Produces(StatusCodes.Status204NoContent);
    }
}



