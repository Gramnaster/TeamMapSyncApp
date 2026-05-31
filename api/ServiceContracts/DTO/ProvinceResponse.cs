namespace ServiceContracts.DTO;

public class ProvinceResponse
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public RegionResponse? Region { get; set; }
}
