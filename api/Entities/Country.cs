namespace Entities;

public class Country
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }

    // Navigation
    public ICollection<Region> Regions { get; } = [];
}
