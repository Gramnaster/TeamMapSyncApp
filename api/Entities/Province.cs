namespace Entities;

public class Province
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }

    // Foreign keys
    public string? RegionId { get; set; }

    // Navigation
    public Region? Region { get; set; }
    public ICollection<LocalGovernmentUnit> LocalGovernmentUnits { get; } = [];
}
