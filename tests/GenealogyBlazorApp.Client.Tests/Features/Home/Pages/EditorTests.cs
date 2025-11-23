using Bunit;
using FluentAssertions;
using GenealogyBlazorApp.Client.Features.Home.Pages;
using GenealogyBlazorApp.Shared.DTOs;
using GenealogyBlazorApp.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace GenealogyBlazorApp.Client.Tests.Features.Home.Pages;

public class EditorTests : BunitContext
{
    private readonly Mock<IPublicHomeService> _mockPublicHomeService;
    private readonly Mock<IAdminHomeService> _mockAdminHomeService;

    public EditorTests()
    {
        _mockPublicHomeService = new Mock<IPublicHomeService>();
        _mockAdminHomeService = new Mock<IAdminHomeService>();

        Services.AddSingleton(_mockPublicHomeService.Object);
        Services.AddSingleton(_mockAdminHomeService.Object);
    }

    [Fact]
    public void Renders_Loading_State_Initially()
    {
        // Arrange
        _mockPublicHomeService.Setup(s => s.GetPublicHomeContentAsync())
            .ReturnsAsync((PublicHomeContentDto?)null); // Simulate loading or empty

        // Act
        var cut = Render<Editor>();

        // Assert
        cut.Find("div.spinner-border").Should().NotBeNull();
    }

    [Fact]
    public void Renders_Content_After_Loading()
    {
        // Arrange
        var content = new PublicHomeContentDto
        {
            SiteTitle = "Test Title",
            Tagline = "Test Tagline",
            AboutContent = "Test About",
            HeroImagePath = "/images/hero.jpg",
            ProfileImagePath = "/images/profile.jpg",
            SidebarLinks = "[]"
        };

        _mockPublicHomeService.Setup(s => s.GetPublicHomeContentAsync())
            .ReturnsAsync(content);

        // Act
        var cut = Render<Editor>();

        // Assert
        cut.WaitForState(() => cut.FindAll("div.spinner-border").Count == 0);
        
        cut.Find("input#siteTitle").GetAttribute("value").Should().Be("Test Title");
        cut.Find("input#tagline").GetAttribute("value").Should().Be("Test Tagline");
    }

    [Fact]
    public async Task Save_Changes_Calls_Service()
    {
        // Arrange
        var content = new PublicHomeContentDto
        {
            SiteTitle = "Original Title",
            Tagline = "Original Tagline",
            AboutContent = "Original About",
            HeroImagePath = "/images/hero.jpg",
            ProfileImagePath = "/images/profile.jpg",
            SidebarLinks = "[]"
        };

        _mockPublicHomeService.Setup(s => s.GetPublicHomeContentAsync())
            .ReturnsAsync(content);

        var cut = Render<Editor>();
        cut.WaitForState(() => cut.FindAll("div.spinner-border").Count == 0);

        // Act
        // Change a value
        cut.Find("input#siteTitle").Change("New Title");
        
        // Click save
        cut.Find("button.btn-primary").Click();

        // Assert
        _mockAdminHomeService.Verify(s => s.UpdateHomeContentAsync(It.Is<UpdateHomeContentRequest>(r => 
            r.SiteTitle == "New Title" &&
            r.Tagline == "Original Tagline"
        )), Times.Once);
    }
}
