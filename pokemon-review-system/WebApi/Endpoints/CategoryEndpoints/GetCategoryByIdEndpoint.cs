using Service.DTOs;
using Service.Interfaces;

namespace WebApi.Endpoints.CategoryEndpoints;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/Category/{id}", async (int id, ICategoryService categoryService) =>
        {
            var category = await categoryService.GetByIdAsync(id);
            return category is not null ? Results.Ok(category) : Results.NotFound();
        })
        .WithTags(Tags.Category)
        .WithName("GetCategoryByIdMinimal")
        .Produces<CategoryDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
