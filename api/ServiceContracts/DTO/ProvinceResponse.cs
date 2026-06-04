using Entities;

namespace ServiceContracts.DTO;

public class ProvinceResponse
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public RegionResponse? Region { get; set; }
    public static ProvinceResponse? ToProvinceResponse(Province? p)
    {
        if (p is null) return null;

        return new ProvinceResponse
        {
            Id = p.Id,
            Name = p.Name,
            Region = RegionResponse.ToRegionResponse(p.Region),
        };
    }
}
