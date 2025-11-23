using System.ComponentModel.DataAnnotations;

namespace GenealogyBlazorApp.Shared.DTOs;

public class UpdateHomeContentRequest
{
    [Required]
    [StringLength(100)]
    public string SiteTitle { get; set; } = string.Empty;

    [Required]
    [StringLength(250)]
    public string Tagline { get; set; } = string.Empty;

    [Required]
    public string AboutContent { get; set; } = string.Empty;

    public string? HeroImagePath { get; set; }

    public string? ProfileImagePath { get; set; }

    public string SidebarLinks { get; set; } = string.Empty;
}
