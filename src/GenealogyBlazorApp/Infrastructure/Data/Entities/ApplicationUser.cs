using Microsoft.AspNetCore.Identity;

namespace GenealogyBlazorApp.Infrastructure.Data.Entities;

/// <summary>
/// Application user extending IdentityUser for admin authentication
/// </summary>
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? LastLoginAt { get; set; }
    
    public bool IsActive { get; set; } = true;
}