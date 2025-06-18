using System.Security.Claims;
using Infrastructure;
using Infrastructure.Data;
using Service;
using WebApi;
using WebApi.Helpers;
using WebApi.Helpers.ExceptionHandlers;

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

// services end here
var app = builder.Build();

// middleware starts from here

#region Seed Data
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using var scope = scopedFactory.CreateScope();
    var service = scope.ServiceProvider.GetService<Seed>();
    service.SeedDataContext();
}

#endregion

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.MapControllers();
app.UseExceptionHandler();

app.Run();
