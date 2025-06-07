var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// services end here
var app = builder.Build();

// middleware starts from here
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();


app.Run();
