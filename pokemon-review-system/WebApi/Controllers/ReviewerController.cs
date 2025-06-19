using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Shared.Helpers;
using Shared.ResourceParameters;
using WebApi.Helpers.Extensions;
using WebApi.Helpers.PaginationHelper;
using WebApi.Helpers.Validation;

namespace WebApi.Controllers;

[Route("api/Reviewers")]
[ApiController]
public class ReviewerController(
    IReviewerService reviewerService,
    IValidator<ReviewerDto> reviewerValidator,
    IPaginationHelper<ReviewerDto, ReviewerResourceParameters> paginationHelper)
    : ControllerBase
{
    // GET ALL ASYNC
    [HttpGet(Name = "GetAllReviewers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] ReviewerResourceParameters resourceParameters)    {
        var reviewers = await reviewerService.GetAllAsync(resourceParameters);
        paginationHelper
            .CreateMetaDataHeader(
                reviewers.Value, resourceParameters, Response.Headers, Url, "GetAllReviewers");
        return reviewers.ToActionResult();
    }

    // GET BY ID ASYNC
    [HttpGet("{id}", Name = "GetReviewerById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await reviewerService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    // POST ASYNC
    [HttpPost(Name = "AddReviewer")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAsync([FromBody] ReviewerDto reviewerDto)
    {
        var inputValid = await ValidationHelper.ValidateAndReportAsync(reviewerValidator, reviewerDto, "Input");
        if (!inputValid.IsSuccess)
            return inputValid.ToActionResult();
        var result = await reviewerService.AddAsync(reviewerDto);
        if (!result.IsSuccess)
            return result.ToActionResult();
        return CreatedAtRoute("GetReviewerById", new { id = result.Value.Id }, result.Value);
    }

    // PATCH ASYNC
    [HttpPatch("{id}", Name = "UpdateReviewer")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync(int id, JsonPatchDocument<ReviewerDto> patchDoc)
    {
        var reviewerResult = await reviewerService.GetByIdAsync(id);
        if (!reviewerResult.IsSuccess)
            return reviewerResult.ToActionResult();

        var reviewerToPatch = reviewerResult.Value;
        var (patchedDto, validationResult) = this.HandlePatch(reviewerToPatch, patchDoc);
        if (!validationResult.IsSuccess)
            return validationResult.ToActionResult();

        var inputValid = await ValidationHelper.ValidateAndReportAsync(reviewerValidator, patchedDto, "input");
        if (!inputValid.IsSuccess)
            return inputValid.ToActionResult();

        patchedDto.Id = id;
        var result = await reviewerService.UpdateAsync(patchedDto);
        if (!result.IsSuccess)
            return result.ToActionResult();

        return NoContent();
    }

    // DELETE ASYNC
    [HttpDelete("{id}", Name = "DeleteReviewer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var reviewerResult = await reviewerService.GetByIdAsync(id);
        if (!reviewerResult.IsSuccess)
            return reviewerResult.ToActionResult();
        var result = await reviewerService.DeleteAsync(reviewerResult.Value);
        if (!result.IsSuccess)
            return result.ToActionResult();
        return NoContent();
    }
}
