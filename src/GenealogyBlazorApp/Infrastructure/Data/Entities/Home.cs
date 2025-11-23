namespace GenealogyBlazorApp.Infrastructure.Data.Entities;

/// <summary>
/// Homepage content entity for managing editable homepage sections
/// </summary>
public class Home : BaseEntity
{
    public string SiteTitle { get; set; } = string.Empty;
    
    public string Tagline { get; set; } = string.Empty;
    
    public string? HeroImagePath { get; set; }
    
    public string AboutContent { get; set; } = string.Empty;
    
    public string AboutSectionTitle { get; set; } = "About Our History";

    public string? ProfileImagePath { get; set; }
    
    public string ProfileImageCaption { get; set; } = "The Archivist";
    
    public string SidebarLinks { get; set; } = string.Empty; // JSON serialized links
    
    // County Cards
    public string HuronTitle { get; set; } = "Huron County";
    public string? HuronImagePath { get; set; }
    
    public string SanilacTitle { get; set; } = "Sanilac County";
    public string? SanilacImagePath { get; set; }
    
    public string TuscolaTitle { get; set; } = "Tuscola County";
    public string? TuscolaImagePath { get; set; }

    public bool IsActive { get; set; } = true;
}