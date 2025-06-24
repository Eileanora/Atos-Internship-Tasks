using System.Security.Claims;
using System.Text;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Service.DTOs;
using Shared.ResourceParameters;
using WebApi.Filters;
using WebApi.Helpers.ExceptionHandlers;
using WebApi.Helpers.PaginationHelper;

namespace WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<JsonReaderExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddScoped<IPaginationHelper<PokemonDto, PokemonResourceParameters>,
                PaginationHelper<PokemonDto, PokemonResourceParameters>>();
        
        services.AddScoped<IPaginationHelper<OwnerDto, OwnerResourceParameters>,
            PaginationHelper<OwnerDto, OwnerResourceParameters>>();
        
        services.AddScoped<IPaginationHelper<ReviewerDto, ReviewerResourceParameters>,
            PaginationHelper<ReviewerDto, ReviewerResourceParameters>>();
        
        services.AddScoped<IPaginationHelper<ReviewDto, ReviewResourceParameters>,
            PaginationHelper<ReviewDto, ReviewResourceParameters>>();

        #region Configure JWT Authentication
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = configuration.GetValue<bool>("Jwt:ValidateIssuer"),
                    ValidateAudience = configuration.GetValue<bool>("Jwt:ValidateAudience"),
                    ValidateLifetime = configuration.GetValue<bool>("Jwt:ValidateLifetime"),
                    ValidateIssuerSigningKey = configuration.GetValue<bool>("Jwt:ValidateIssuerSigningKey"),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero,
                    RoleClaimType = ClaimTypes.Role,
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        #endregion
        
        services.AddScoped<AuthOwnerByIdFilter>();
        
        return services;
    }
}
