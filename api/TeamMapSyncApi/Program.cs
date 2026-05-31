using Entities;
using Entities.SeedData;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var dataDir = builder.Configuration["SeedDataDirectory"]
        ?? Path.Combine(builder.Environment.ContentRootPath, "SeedData");
    await DatabaseSeeder.SeedAsync(db, dataDir);
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

await app.RunAsync().ConfigureAwait(false);

