using Entities;

namespace ServiceContracts.DTO;

public class BarangayResponse
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public LocalGovernmentUnitResponse? LocalGovernmentUnit { get; set; }
    public static BarangayResponse? ToBarangayResponse(Barangay? b)
    {
        if (b is null) return null;
        return new BarangayResponse
        {
            Id = b.Id,
            Name = b.Name,
            LocalGovernmentUnit = LocalGovernmentUnitResponse.ToLguResponse(b.LocalGovernmentUnit),
        };
    }
}
