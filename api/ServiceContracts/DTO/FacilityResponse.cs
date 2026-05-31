using Entities;

namespace ServiceContracts.DTO;

public class FacilityResponse
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
    public FacilityTypeResponse? FacilityType { get; set; }
    public BarangayResponse? Barangay { get; set; }
}


