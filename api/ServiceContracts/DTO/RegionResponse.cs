using Entities;

namespace ServiceContracts.DTO;

public class RegionResponse
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public string? CountryId { get; set; }
    public static RegionResponse? ToRegionResponse(Region? r)
    {
        if (r is null) return null;
        return new RegionResponse
        {
            Id = r.Id,
            Name = r.Name,
            CountryId = r.CountryId,
        };
    }
}
