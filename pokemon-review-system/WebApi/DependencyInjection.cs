using Shared.DTOs;
using Shared.ResourceParameters;
using WebApi.Helpers.ExceptionHandlers;
using WebApi.Helpers.PaginationHelper;

namespace WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddScoped<IPaginationHelper<PokemonDto, PokemonResourceParameters>,
                PaginationHelper<PokemonDto, PokemonResourceParameters>>();
        
        services.AddScoped<IPaginationHelper<OwnerDto, OwnerResourceParameters>,
            PaginationHelper<OwnerDto, OwnerResourceParameters>>();
        
        services.AddScoped<IPaginationHelper<ReviewerDto, ReviewerResourceParameters>,
            PaginationHelper<ReviewerDto, ReviewerResourceParameters>>();
        
        services.AddScoped<IPaginationHelper<ReviewDto, ReviewResourceParameters>,
            PaginationHelper<ReviewDto, ReviewResourceParameters>>();
        
        return services;
    }
}
