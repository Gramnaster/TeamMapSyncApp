using Microsoft.EntityFrameworkCore;

namespace Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Region> Regions => Set<Region>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<LocalGovernmentUnit> LocalGovernmentUnits => Set<LocalGovernmentUnit>();
    public DbSet<Barangay> Barangays => Set<Barangay>();
    public DbSet<FacilityType> FacilityTypes => Set<FacilityType>();
    public DbSet<FacilityRole> FacilityRoles => Set<FacilityRole>();
    public DbSet<Facility> Facilities => Set<Facility>();
    public DbSet<User> Users => Set<User>();
    public DbSet<FacilityUser> FacilityUsers => Set<FacilityUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.Entity<FacilityUser>()
            .HasKey(fu => new { fu.UserId, fu.FacilityId });

        modelBuilder.Entity<FacilityUser>()
            .HasOne(fu => fu.User)
            .WithMany(u => u.FacilityUsers)
            .HasForeignKey(fu => fu.UserId);

        modelBuilder.Entity<FacilityUser>()
            .HasOne(fu => fu.Facility)
            .WithMany(f => f.FacilityUsers)
            .HasForeignKey(fu => fu.FacilityId);

        modelBuilder.Entity<FacilityUser>()
            .HasOne(fu => fu.FacilityRole)
            .WithMany(fr => fr.FacilityUsers)
            .HasForeignKey(fu => fu.FacilityRoleId);

        modelBuilder.Entity<Facility>()
            .Property(f => f.FacilityStatus)
            .HasConversion<string>();

        modelBuilder.Entity<LocalGovernmentUnit>()
            .Property(lgu => lgu.Type)
            .HasConversion<string>();
    }
}
