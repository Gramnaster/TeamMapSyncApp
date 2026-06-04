namespace Entities;

public class LocalGovernmentUnit
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public LocalGovernmentUnitType Type { get; set; }

    // Foreign keys
    public string? ProvinceId { get; set; }

    // Navigation
    public Province? Province { get; set; }
    public ICollection<Barangay> Barangays { get; } = [];
}
