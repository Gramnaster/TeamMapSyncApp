namespace Entities;

public class Facility
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public string? ContactNumber { get; set; }
    public string? Email { get; set; }
    public FacilityStatus FacilityStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Foreign keys
    public string? FacilityTypeId { get; set; }
    public string? BarangayId { get; set; }

    // Navigation
    public FacilityType? FacilityType { get; set; }
    public Barangay? Barangay { get; set; }
    public ICollection<FacilityUser> FacilityUsers { get; } = [];
}