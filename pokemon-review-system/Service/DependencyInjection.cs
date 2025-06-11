using Microsoft.Extensions.DependencyInjection;
using Service.Managers.CategoryManager;
using Service.Managers.CountryManager;
using Service.Managers.PokemonManager;

namespace Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IPokemonManager, PokemonManager>();
        services.AddScoped<ICountryManager, CountryManager>();
        services.AddScoped<ICategoryManager, CategoryManager>();
        return services;
    }
}