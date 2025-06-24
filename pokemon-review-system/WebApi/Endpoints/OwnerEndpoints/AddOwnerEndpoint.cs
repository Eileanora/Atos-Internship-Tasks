using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Shared.Helpers;
using WebApi.Helpers.Extensions;

namespace WebApi.Endpoints.OwnerEndpoints;

public class AddOwnerEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/Owners", async (
                [FromBody] CreateOwnerDto ownerDto,
                [FromServices] IOwnerService ownerManager,
                [FromServices] IValidator<CreateOwnerDto> createOwnerValidator) =>
        {
            var inputValid = await ValidationHelper.ValidateAndReportAsync(createOwnerValidator, ownerDto, "Input");
            if (!inputValid.IsSuccess)
                return inputValid.ToResults();
            var result = await ownerManager.AddAsync(ownerDto);
            if (!result.IsSuccess)
                return result.ToResults();
            return Results.CreatedAtRoute("GetOwnerByIdMinimal", new { id = result.Value.Id }, result.Value);
        })
        .WithName("AddOwnerMinimal")
        .WithTags(Tags.Owner)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status201Created);
    }
}
