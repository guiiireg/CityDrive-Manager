using CityDriveManager.Data;
using CityDriveManager.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure MySQL DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=localhost;Database=citydrive;User=root;Password=;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container.
builder.Services.AddSingleton<DataManager>();
builder.Services.AddSingleton<PersistenceService>();
builder.Services.AddScoped<SearchService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();

