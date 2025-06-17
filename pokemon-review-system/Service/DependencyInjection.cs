using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Service.Managers.AuthManager;
using Shared.DTOs;
using Service.Managers.CategoryManager;
using Service.Managers.CountryManager;
using Service.Managers.OwnerManager;
using Service.Managers.PokemonManager;
using Service.Managers.ReviewerManager;
using Service.Managers.ReviewManager;
using Service.Validators;

namespace Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IPokemonManager, PokemonManager>();
        services.AddScoped<ICountryManager, CountryManager>();
        services.AddScoped<ICategoryManager, CategoryManager>();
        services.AddScoped<IOwnerManager, OwnerManager>();
        services.AddScoped<IReviewerManager, ReviewerManager>();
        services.AddScoped<IReviewManager, ReviewManager>();
        services.AddScoped<IAuthManager, AuthManager>();
        services.AddValidatorsFromAssemblyContaining<PokemonDtoValidator>();

        return services;
    }
}
