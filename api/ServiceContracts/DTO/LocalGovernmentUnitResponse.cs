using Entities;

namespace ServiceContracts.DTO;

public class LocalGovernmentUnitResponse
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public LocalGovernmentUnitType Type { get; set; }
    public ProvinceResponse? Province { get; set; }

    public static LocalGovernmentUnitResponse? ToLguResponse(LocalGovernmentUnit? l)
    {
        if (l is null) return null;
        return new LocalGovernmentUnitResponse
        {
            Id = l.Id,
            Name = l.Name,
            Type = l.Type,
            Province = ProvinceResponse.ToProvinceResponse(l.Province),
        };
    }
}
