using Entities;
using Entities.SeedData;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddHttpClient();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var dataDir = builder.Configuration["SeedDataDirectory"]
        ?? Path.Combine(builder.Environment.ContentRootPath, "SeedData");
    await DatabaseSeeder.SeedAsync(db, dataDir);
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

// Log all registered endpoints at startup
var endpointDataSource = app.Services.GetRequiredService<EndpointDataSource>();
foreach (var endpoint in endpointDataSource.Endpoints)
{
    var displayName = endpoint.DisplayName ?? "Unknown";
    var methods = endpoint.Metadata
        .OfType<HttpMethodMetadata>()
        .FirstOrDefault()
        ?.HttpMethods
        ?? ["ANY"];

    Console.WriteLine($"{string.Join(", ", methods)} {displayName}");
}

await app.RunAsync().ConfigureAwait(false);