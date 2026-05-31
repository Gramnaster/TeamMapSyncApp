using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Entities.SeedData;

public static class DatabaseSeeder
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static async Task SeedAsync(AppDbContext db, string dataDirectory)
    {
        ArgumentNullException.ThrowIfNull(db);
        await db.Database.MigrateAsync().ConfigureAwait(false);

        if (await db.Countries.AnyAsync().ConfigureAwait(false)) return;

        var countries = Load<Country>(dataDirectory, "countries.json");
        var regions = Load<Region>(dataDirectory, "regions.json");
        var provinces = Load<Province>(dataDirectory, "provinces.json");
        var lgus = Load<LocalGovernmentUnit>(dataDirectory, "local-government-units.json");
        var barangays = Load<Barangay>(dataDirectory, "barangays.json");
        var facilityTypes = Load<FacilityType>(dataDirectory, "facility-types.json");
        var facilityRoles = Load<FacilityRole>(dataDirectory, "facility-roles.json");
        var facilities = Load<Facility>(dataDirectory, "facilities.json");
        var users = Load<User>(dataDirectory, "users.json");
        var facilityUsers = Load<FacilityUser>(dataDirectory, "facility-users.json");

        await db.Countries.AddRangeAsync(countries).ConfigureAwait(false);
        await db.SaveChangesAsync().ConfigureAwait(false);

        await db.Regions.AddRangeAsync(regions).ConfigureAwait(false);
        await db.SaveChangesAsync().ConfigureAwait(false);

        await db.Provinces.AddRangeAsync(provinces).ConfigureAwait(false);
        await db.SaveChangesAsync().ConfigureAwait(false);

        await db.LocalGovernmentUnits.AddRangeAsync(lgus).ConfigureAwait(false);
        await db.SaveChangesAsync().ConfigureAwait(false);

        await db.Barangays.AddRangeAsync(barangays).ConfigureAwait(false);
        await db.SaveChangesAsync().ConfigureAwait(false);

        await db.FacilityTypes.AddRangeAsync(facilityTypes).ConfigureAwait(false);
        await db.FacilityRoles.AddRangeAsync(facilityRoles).ConfigureAwait(false);
        await db.SaveChangesAsync().ConfigureAwait(false);

        await db.Facilities.AddRangeAsync(facilities).ConfigureAwait(false);
        await db.SaveChangesAsync().ConfigureAwait(false);

        await db.Users.AddRangeAsync(users).ConfigureAwait(false);
        await db.SaveChangesAsync().ConfigureAwait(false);

        await db.FacilityUsers.AddRangeAsync(facilityUsers).ConfigureAwait(false);
        await db.SaveChangesAsync().ConfigureAwait(false);
    }

    private static List<T> Load<T>(string directory, string fileName)
    {
        var path = Path.Combine(directory, fileName);
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(json, JsonOptions) ?? [];
    }
}
