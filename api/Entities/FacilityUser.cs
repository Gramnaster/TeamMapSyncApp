namespace Entities;

public class FacilityUser
{
    public Guid UserId { get; set; }
    public Guid FacilityId { get; set; }
    public string? FacilityRoleId { get; set; }
    public DateTime AssignedAt { get; set; }

    // Navigation
    public User? User { get; set; }
    public Facility? Facility { get; set; }
    public FacilityRole? FacilityRole { get; set; }
}
