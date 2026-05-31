namespace Entities;

public class Barangay
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }

    // Foreign keys
    public string? LocalGovernmentUnitId { get; set; }

    // Navigation
    public LocalGovernmentUnit? LocalGovernmentUnit { get; set; }
    public ICollection<Facility> Facilities { get; } = [];
}
