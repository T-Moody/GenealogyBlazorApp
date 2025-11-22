namespace GenealogyBlazorApp.Shared.DTOs;

public class HomeContentDto
{
    public int Id { get; set; }
    public string SiteTitle { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string AboutContent { get; set; } = string.Empty;
    public string SidebarLinks { get; set; } = string.Empty;
    public string? HeroImagePath { get; set; }
    public string? ProfileImagePath { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
}
