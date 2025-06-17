using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;
using Shared.DTOs;
using Service.Services;
using Service.Validators;

namespace Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IPokemonManager, PokemonService>();
        services.AddScoped<ICountryManager, CountryService>();
        services.AddScoped<ICategoryManager, CategoryService>();
        services.AddScoped<IOwnerManager, OwnerService>();
        services.AddScoped<IReviewerManager, ReviewerService>();
        services.AddScoped<IReviewManager, ReviewService>();
        services.AddScoped<IAuthManager, AuthService>();
        services.AddScoped<IGenerateTokenService, GenerateTokenService>();
        services.AddValidatorsFromAssemblyContaining<PokemonDtoValidator>();

        return services;
    }
}
