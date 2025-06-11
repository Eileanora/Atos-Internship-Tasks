using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.Interfaces;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.EnableSensitiveDataLogging();
            options.LogTo(Console.WriteLine, LogLevel.Information);
            options.EnableDetailedErrors();
        });
        services.AddTransient<Seed>();
        services.AddScoped<IPokemonRepository, PokemonRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        // services.AddScoped<>();
        return services;
    }
}
