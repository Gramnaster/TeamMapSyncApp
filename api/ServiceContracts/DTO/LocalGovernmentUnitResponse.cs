using Entities;

namespace ServiceContracts.DTO;

public class LocalGovernmentUnitResponse
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public LocalGovernmentUnitType Type { get; set; }
    public ProvinceResponse? Province { get; set; }
}
