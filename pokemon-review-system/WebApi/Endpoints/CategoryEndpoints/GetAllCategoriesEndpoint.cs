using Service.DTOs;
using Service.Interfaces;

namespace WebApi.Endpoints.CategoryEndpoints;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/Category", async (ICategoryService categoryService) =>
        {
            var categories = await categoryService.GetAllAsync();
            return Results.Ok(categories);
        })
        .WithTags(Tags.Category)
        .WithName("GetAllCategoriesMinimal")
        .Produces<IEnumerable<CategoryDto>>(StatusCodes.Status200OK);
    }
}
