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

    public static FacilityResponse? ToResponse(Facility f)
    {
        if (f is null) return null;

        return new FacilityResponse
        {
            Id = f.Id,
            Code = f.Code,
            Name = f.Name,
            Latitude = f.Latitude,
            Longitude = f.Longitude,
            Address = f.Address,
            PostalCode = f.PostalCode,
            ContactNumber = f.ContactNumber,
            Email = f.Email,
            FacilityStatus = f.FacilityStatus,
            CreatedAt = f.CreatedAt,
            UpdatedAt = f.UpdatedAt,
            FacilityType = f.FacilityType is null ? null : new FacilityTypeResponse
            {
                Id = f.FacilityType.Id,
                Name = f.FacilityType.Name,
            },
            Barangay = BarangayResponse.ToBarangayResponse(f.Barangay),
        };
    }
}