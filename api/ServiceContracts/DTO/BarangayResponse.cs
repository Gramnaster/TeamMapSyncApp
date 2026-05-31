namespace ServiceContracts.DTO;

public class BarangayResponse
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public LocalGovernmentUnitResponse? LocalGovernmentUnit { get; set; }
}
