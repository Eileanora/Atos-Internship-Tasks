using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;

namespace Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        // services.AddScoped<IPokemonRepository, >();
        
        return services;
    }
}