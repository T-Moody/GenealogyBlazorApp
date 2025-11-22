namespace GenealogyBlazorApp.Shared.DTOs;

/// <summary>
/// DTO for publicly displayed home page content.
/// </summary>
public class PublicHomeContentDto
{
    public string SiteTitle { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string AboutContent { get; set; } = string.Empty;
    public string SidebarLinks { get; set; } = string.Empty; // JSON serialized links
    public string? HeroImagePath { get; set; }
    public string? ProfileImagePath { get; set; }
}
