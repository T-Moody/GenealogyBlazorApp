namespace GenealogyBlazorApp.Shared.DTOs;

/// <summary>
/// DTO for publicly displayed home page content.
/// </summary>
public class PublicHomeContentDto
{
    public string SiteTitle { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string AboutContent { get; set; } = string.Empty;
    public string AboutSectionTitle { get; set; } = string.Empty;
    public string SidebarLinks { get; set; } = string.Empty; // JSON serialized links
    public string? HeroImagePath { get; set; }
    public string? ProfileImagePath { get; set; }
    public string ProfileImageCaption { get; set; } = string.Empty;

    // County Cards
    public string HuronTitle { get; set; } = "Huron County";
    public string? HuronImagePath { get; set; }
    public string SanilacTitle { get; set; } = "Sanilac County";
    public string? SanilacImagePath { get; set; }
    public string TuscolaTitle { get; set; } = "Tuscola County";
    public string? TuscolaImagePath { get; set; }
}
