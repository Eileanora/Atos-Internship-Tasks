using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Infrastructure.Interceptors;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IPokemonOwnerRepository, PokemonOwnerRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IReviewerRepository, ReviewerRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<ISortHelper<Pokemon>, SortHelper<Pokemon>>();
        services.AddScoped<ISortHelper<Owner>, SortHelper<Owner>>();
        services.AddScoped<ISortHelper<Reviewer>, SortHelper<Reviewer>>();
        services.AddScoped<ISortHelper<Review>, SortHelper<Review>>();
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<DataContext>();

        // services.AddScoped<>();
        return services;
    }
}
