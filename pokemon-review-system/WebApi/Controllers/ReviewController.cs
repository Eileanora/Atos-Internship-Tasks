using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Service.Managers.ReviewManager;
using Shared.DTOs;
using Shared.ResourceParameters;
using WebApi.Helpers.Extensions;
using WebApi.Helpers.PaginationHelper;
using WebApi.Helpers.Validation;

namespace WebApi.Controllers;

[Route("api/Reviews")]
[ApiController]
public class ReviewController(
    IReviewManager reviewManager,
    IValidator<ReviewDto> reviewValidator,
    IPaginationHelper<ReviewDto, ReviewResourceParameters> paginationHelper)
    : ControllerBase
{
    // GET ALL
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync([FromQuery] ReviewResourceParameters resourceParameters)
    {
        var result = await reviewManager.GetAllAsync(resourceParameters);
        
        paginationHelper.
            CreateMetaDataHeader(
                result.Value, resourceParameters, Response.Headers, Url, "GetAllReviews");
        
        return result.ToActionResult();
    }
    
    // GET BY ID
    [HttpGet("{id}", Name = "GetReviewById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await reviewManager.GetByIdAsync(id);
        return result.ToActionResult();
    }

    // POST
    [HttpPost(Name = "AddReview")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddAsync([FromBody] ReviewDto reviewDto)
    {
        var inputValid = await ValidationHelper.ValidateAndReportAsync(reviewValidator, reviewDto, "Input");
        if (!inputValid.IsSuccess)
            return inputValid.ToActionResult();
        var result = await reviewManager.AddAsync(reviewDto);
        if (!result.IsSuccess)
            return result.ToActionResult();
        return CreatedAtRoute("GetReviewById", new { id = result.Value.Id }, result.Value);
    }

    // DELETE
    [HttpDelete("{id}", Name = "DeleteReview")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var reviewResult = await reviewManager.GetByIdAsync(id);
        if (!reviewResult.IsSuccess)
            return reviewResult.ToActionResult();
        var result = await reviewManager.DeleteAsync(reviewResult.Value);
        if (!result.IsSuccess)
            return result.ToActionResult();
        return NoContent();
    }
}
