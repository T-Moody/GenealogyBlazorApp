using GenealogyBlazorApp.Shared.DTOs;

namespace GenealogyBlazorApp.Client.Features.Home.Models;

public class HomeState
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

    public bool IsLoading { get; set; } = true;

    public void FromDto(PublicHomeContentDto dto)
    {
        SiteTitle = dto.SiteTitle;
        Tagline = dto.Tagline;
        AboutContent = dto.AboutContent;
        SidebarLinks = dto.SidebarLinks;
        HeroImagePath = dto.HeroImagePath;
        ProfileImagePath = dto.ProfileImagePath;
        
        HuronTitle = dto.HuronTitle;
        HuronImagePath = dto.HuronImagePath;
        SanilacTitle = dto.SanilacTitle;
        SanilacImagePath = dto.SanilacImagePath;
        TuscolaTitle = dto.TuscolaTitle;
        TuscolaImagePath = dto.TuscolaImagePath;
    }

    public void FromDto(HomeContentDto dto)
    {
        SiteTitle = dto.SiteTitle;
        Tagline = dto.Tagline;
        AboutContent = dto.AboutContent;
        SidebarLinks = dto.SidebarLinks;
        HeroImagePath = dto.HeroImagePath;
        ProfileImagePath = dto.ProfileImagePath;
        
        HuronTitle = dto.HuronTitle;
        HuronImagePath = dto.HuronImagePath;
        SanilacTitle = dto.SanilacTitle;
        SanilacImagePath = dto.SanilacImagePath;
        TuscolaTitle = dto.TuscolaTitle;
        TuscolaImagePath = dto.TuscolaImagePath;
    }
}
