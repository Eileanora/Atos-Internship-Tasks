using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);

// services end here
var app = builder.Build();

// middleware starts from here


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();


app.Run();
