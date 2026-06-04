namespace Entities;

public class User
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? HomeAddress { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation
    public ICollection<FacilityUser> FacilityUsers { get; } = [];
}
