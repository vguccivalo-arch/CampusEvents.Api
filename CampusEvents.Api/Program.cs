using CampusEvents.Api.Middlewares;
using CampusEvents.Api.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add OpenAPI support (built into modern .NET)
builder.Services.AddOpenApi();

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// --- DATABASE SEEDER EXECUTION ---
// Automatically seeds the database on application startup if empty
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated(); // Ensure the database is created

    DbSeeder.SeedDataBase(dbContext); // Seed the database with initial data
}
// ---------------------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Map OpenAPI and Scalar for development mode (Access via /scalar)
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseMiddleware<ApiKeyMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllers();

app.Run();