namespace GenealogyBlazorApp.Shared.DTOs;

public class HomeContentDto
{
    public string SiteTitle { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string AboutContent { get; set; } = string.Empty;
    public string SidebarLinks { get; set; } = string.Empty;
    public string? HeroImagePath { get; set; }
    public string? ProfileImagePath { get; set; }
    
    // County Cards
    public string HuronTitle { get; set; } = "Huron County";
    public string? HuronImagePath { get; set; }
    public string SanilacTitle { get; set; } = "Sanilac County";
    public string? SanilacImagePath { get; set; }
    public string TuscolaTitle { get; set; } = "Tuscola County";
    public string? TuscolaImagePath { get; set; }

    public DateTime LastUpdated { get; set; }
    public string LastUpdatedBy { get; set; } = string.Empty;
}
