using Microsoft.AspNetCore.Identity;

namespace RWMS.Models.Domain;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    // Used for clients to store their business/restaurant name
    public string? CompanyName { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Computed helper — not stored in DB
    public string FullName => $"{FirstName} {LastName}".Trim();
}
