namespace Entities;

public class FacilityRole
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }

    // Navigation
    public ICollection<FacilityUser> FacilityUsers { get; } = [];
}
