using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Service.DTOs;
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
        services.AddValidatorsFromAssemblyContaining<PokemonDto>();

        return services;
    }
}
