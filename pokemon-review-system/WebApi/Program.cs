using System.Reflection;
using System.Security.Claims;
using Domain.Models;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Service;
using WebApi;
using WebApi.Endpoints;
using WebApi.Endpoints.PokemonEndpoints;
using WebApi.Helpers;
using WebApi.Helpers.ExceptionHandlers;
using WebApi.Helpers.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, JsonPatchInputFormatter.GetJsonPatchInputFormatter());
});
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServiceLayer();
builder.Services.AddApiLayer(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityData.AdminUserPolicyName, p =>
    {
        p.RequireClaim(ClaimTypes.Role, "Admin");
    });
    options.AddPolicy(IdentityData.OwnerUserPolicyName, policy => policy.RequireRole("User"));
});
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

// services end here
var app = builder.Build();

// create a route group for minimal APIs
var routeGroup = app.MapGroup("minimal/api");

// middleware starts from here

#region Seed Data
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using var scope = scopedFactory.CreateScope();
    var service = scope.ServiceProvider.GetService<Seed>();
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    service.SeedDataContext(userManager);
}
#endregion

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapEndpoints(routeGroup);
app.UseExceptionHandler();

app.Run();
