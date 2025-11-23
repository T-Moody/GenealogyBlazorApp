using GenealogyBlazorApp.Shared.DTOs;
using System.Text.Json;

namespace GenealogyBlazorApp.Client.Features.Home.Models;

public class LinkModel
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class HomeState
{
    public string SiteTitle { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string AboutContent { get; set; } = string.Empty;
    public string AboutSectionTitle { get; set; } = "About Our History";
    public string SidebarLinks { get; set; } = string.Empty; // Kept for raw access if needed, but primary is Links
    public List<LinkModel> Links { get; set; } = new();
    
    public string? HeroImagePath { get; set; }
    public string? ProfileImagePath { get; set; }
    public string ProfileImageCaption { get; set; } = "The Archivist";
    
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
        AboutSectionTitle = dto.AboutSectionTitle;
        SidebarLinks = dto.SidebarLinks;
        ParseLinks(dto.SidebarLinks);
        
        HeroImagePath = dto.HeroImagePath;
        ProfileImagePath = dto.ProfileImagePath;
        ProfileImageCaption = dto.ProfileImageCaption;
        
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
        AboutSectionTitle = dto.AboutSectionTitle;
        SidebarLinks = dto.SidebarLinks;
        ParseLinks(dto.SidebarLinks);
        
        HeroImagePath = dto.HeroImagePath;
        ProfileImagePath = dto.ProfileImagePath;
        ProfileImageCaption = dto.ProfileImageCaption;
        
        HuronTitle = dto.HuronTitle;
        HuronImagePath = dto.HuronImagePath;
        SanilacTitle = dto.SanilacTitle;
        SanilacImagePath = dto.SanilacImagePath;
        TuscolaTitle = dto.TuscolaTitle;
        TuscolaImagePath = dto.TuscolaImagePath;
    }

    private void ParseLinks(string jsonOrHtml)
    {
        Links.Clear();
        if (string.IsNullOrWhiteSpace(jsonOrHtml)) return;

        try
        {
            // Try parsing as JSON first
            var links = JsonSerializer.Deserialize<List<LinkModel>>(jsonOrHtml);
            if (links != null)
            {
                Links.AddRange(links);
                return;
            }
        }
        catch
        {
            // Not JSON, ignore or handle legacy HTML if strictly necessary, 
            // but for now we assume we are migrating to JSON.
            // If it's HTML, we just start with an empty list or maybe one empty item.
        }
    }
}
