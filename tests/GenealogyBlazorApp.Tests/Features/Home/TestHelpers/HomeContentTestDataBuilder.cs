using GenealogyBlazorApp.Infrastructure.Data.Entities;

namespace GenealogyBlazorApp.Tests.Features.Home.TestHelpers;

public class HomeContentTestDataBuilder
{
    private int _id = 1;
    private string _siteTitle = "Test Site";
    private string _tagline = "Test Tagline";
    private string _aboutContent = "Test About Content";
    private string _sidebarLinks = "[]";
    private string? _heroImagePath = "/images/hero.jpg";
    private string? _profileImagePath = "/images/profile.jpg";
    private bool _isActive = true;
    private DateTime _createdAt = DateTime.UtcNow.AddDays(-10);
    private DateTime _updatedAt = DateTime.UtcNow.AddDays(-1);
    private string _createdBy = "admin";
    private string _updatedBy = "admin";

    public HomeContentTestDataBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public HomeContentTestDataBuilder WithSiteTitle(string siteTitle)
    {
        _siteTitle = siteTitle;
        return this;
    }

    public HomeContentTestDataBuilder WithTagline(string tagline)
    {
        _tagline = tagline;
        return this;
    }

    public HomeContentTestDataBuilder WithAboutContent(string aboutContent)
    {
        _aboutContent = aboutContent;
        return this;
    }

    public HomeContentTestDataBuilder WithSidebarLinks(string sidebarLinks)
    {
        _sidebarLinks = sidebarLinks;
        return this;
    }

    public HomeContentTestDataBuilder WithHeroImagePath(string? heroImagePath)
    {
        _heroImagePath = heroImagePath;
        return this;
    }

    public HomeContentTestDataBuilder WithProfileImagePath(string? profileImagePath)
    {
        _profileImagePath = profileImagePath;
        return this;
    }

    public HomeContentTestDataBuilder WithIsActive(bool isActive)
    {
        _isActive = isActive;
        return this;
    }

    public HomeContentTestDataBuilder WithCreatedAt(DateTime createdAt)
    {
        _createdAt = createdAt;
        return this;
    }

    public HomeContentTestDataBuilder WithUpdatedAt(DateTime updatedAt)
    {
        _updatedAt = updatedAt;
        return this;
    }

    public HomeContentTestDataBuilder WithCreatedBy(string createdBy)
    {
        _createdBy = createdBy;
        return this;
    }

    public HomeContentTestDataBuilder WithUpdatedBy(string updatedBy)
    {
        _updatedBy = updatedBy;
        return this;
    }

    public HomeContent Build()
    {
        return new HomeContent
        {
            Id = _id,
            SiteTitle = _siteTitle,
            Tagline = _tagline,
            AboutContent = _aboutContent,
            SidebarLinks = _sidebarLinks,
            HeroImagePath = _heroImagePath,
            ProfileImagePath = _profileImagePath,
            IsActive = _isActive,
            CreatedAt = _createdAt,
            UpdatedAt = _updatedAt,
            CreatedBy = _createdBy,
            UpdatedBy = _updatedBy
        };
    }

    public static HomeContentTestDataBuilder Default() => new();

    public static HomeContentTestDataBuilder Active() => new().WithIsActive(true);

    public static HomeContentTestDataBuilder Inactive() => new().WithIsActive(false);

    public static HomeContentTestDataBuilder WithEmptyImagePaths() => new()
        .WithHeroImagePath(null)
        .WithProfileImagePath(null);
}
}