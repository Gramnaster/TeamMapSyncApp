namespace Entities;

public class Region
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }

    // Foreign keys
    public string? CountryId { get; set; }

    // Navigation
    public Country? Country { get; set; }
    public ICollection<Province> Provinces { get; } = [];
}
