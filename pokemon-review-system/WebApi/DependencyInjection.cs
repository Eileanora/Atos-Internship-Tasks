using WebApi.Helpers.ExceptionHandlers;

namespace WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddExceptionHandler<ValidationExceptionHandler>();

        
        return services;
    }
}
