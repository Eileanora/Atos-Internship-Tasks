using Infrastructure.Data;
using Infrastructure.Interceptors;
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
        services.AddSingleton<UpdateAuditFieldsInterceptor>();
        services.AddDbContext<DataContext>((sp, options) =>
        {
            var auditInterceptor = sp.GetRequiredService<UpdateAuditFieldsInterceptor>();
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(auditInterceptor);
            options.EnableSensitiveDataLogging();
            // options.LogTo(Console.WriteLine, LogLevel.Information);
            options.EnableDetailedErrors();
        });
        services.AddTransient<Seed>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPokemonRepository, PokemonRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        // services.AddScoped<>();
        return services;
    }
}
