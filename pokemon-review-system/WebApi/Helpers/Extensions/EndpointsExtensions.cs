using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApi.Endpoints;

namespace WebApi.Helpers.Extensions;

public static class EndpointsExtensions
{
    // MapEnpoints
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var endpointServiceDescriptors = assembly.DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();
        services.TryAddEnumerable(endpointServiceDescriptors); // try to register them using DI
        return services;
    }
    
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>(); // Ensure all endpoints are registered
        // resolve all endpoints implementing IEndpoint

        IEndpointRouteBuilder endpointRouteBuilder = routeGroupBuilder is null? app : routeGroupBuilder;
        foreach (IEndpoint endpoint in endpoints)
            endpoint.MapEndpoints(endpointRouteBuilder); // call MapEndpoints for each endpoint
        return app;
    }
}

