using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;
using Service.Services;
using Service.Validators;

namespace Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IPokemonService, PokemonService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOwnerService, OwnerService>();
        services.AddScoped<IReviewerService, ReviewerService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGenerateTokenService, GenerateTokenService>();
        services.AddValidatorsFromAssemblyContaining<PokemonDtoValidator>();

        return services;
    }
}
