namespace GenealogyBlazorApp.Infrastructure.Data.Entities;

/// <summary>
/// Homepage content entity for managing editable homepage sections
/// </summary>
public class HomeContent : BaseEntity
{
    public string SiteTitle { get; set; } = string.Empty;
    
    public string Tagline { get; set; } = string.Empty;
    
    public string? HeroImagePath { get; set; }
    
    public string AboutContent { get; set; } = string.Empty;
    
    public string? ProfileImagePath { get; set; }
    
    public string SidebarLinks { get; set; } = string.Empty; // JSON serialized links
    
    public bool IsActive { get; set; } = true;
}