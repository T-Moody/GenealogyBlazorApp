using FluentAssertions;
using GenealogyBlazorApp.Features.Home.Queries;
using GenealogyBlazorApp.Infrastructure.Data;
using GenealogyBlazorApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GenealogyBlazorApp.Tests.Features.Home.Queries;

public class GetPublicHomeContentQueryHandlerTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly Mock<ILogger<GetPublicHomeContentQueryHandler>> _mockLogger;
    private readonly GetPublicHomeContentQueryHandler _handler;

    public GetPublicHomeContentQueryHandlerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _mockLogger = new Mock<ILogger<GetPublicHomeContentQueryHandler>>();
        _handler = new GetPublicHomeContentQueryHandler(_context, _mockLogger.Object);
    }

    [Fact]
    public async Task Handle_WithActiveHomeContent_ShouldReturnPublicHomeContentDto()
    {
        // Arrange
        var homeContent = new HomeContent
        {
            Id = 1,
            SiteTitle = "Test Site",
            Tagline = "Test Tagline",
            AboutContent = "Test About Content",
            SidebarLinks = "[]",
            HeroImagePath = "/images/custom-hero.png",
            ProfileImagePath = "/images/profile.png",
            IsActive = true,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = "Test User"
        };

        _context.HomeContent.Add(homeContent);
        await _context.SaveChangesAsync();

        var query = new GetPublicHomeContentQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.SiteTitle.Should().Be("Test Site");
        result.Tagline.Should().Be("Test Tagline");
        result.AboutContent.Should().Be("Test About Content");
        result.SidebarLinks.Should().Be("[]");
        result.HeroImagePath.Should().Be("/images/custom-hero.png");
        result.ProfileImagePath.Should().Be("/images/profile.png");
    }

    [Fact]
    public async Task Handle_WithEmptyHeroImagePath_ShouldSetDefaultHeroImage()
    {
        // Arrange
        var homeContent = new HomeContent
        {
            Id = 1,
            SiteTitle = "Test Site",
            HeroImagePath = null, // or string.Empty
            IsActive = true,
            UpdatedAt = DateTime.UtcNow
        };

        _context.HomeContent.Add(homeContent);
        await _context.SaveChangesAsync();

        var query = new GetPublicHomeContentQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.HeroImagePath.Should().Be("/images/hero-image-thumb.png");
    }

    [Fact]
    public async Task Handle_WithEmptyStringHeroImagePath_ShouldSetDefaultHeroImage()
    {
        // Arrange
        var homeContent = new HomeContent
        {
            Id = 1,
            SiteTitle = "Test Site",
            HeroImagePath = string.Empty,
            IsActive = true,
            UpdatedAt = DateTime.UtcNow
        };

        _context.HomeContent.Add(homeContent);
        await _context.SaveChangesAsync();

        var query = new GetPublicHomeContentQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.HeroImagePath.Should().Be("/images/hero-image-thumb.png");
    }

    [Fact]
    public async Task Handle_WithMultipleHomeContent_ShouldReturnMostRecentActive()
    {
        // Arrange
        var oldContent = new HomeContent
        {
            Id = 1,
            SiteTitle = "Old Site",
            IsActive = true,
            UpdatedAt = DateTime.UtcNow.AddDays(-1)
        };

        var newContent = new HomeContent
        {
            Id = 2,
            SiteTitle = "New Site",
            IsActive = true,
            UpdatedAt = DateTime.UtcNow
        };

        _context.HomeContent.AddRange(oldContent, newContent);
        await _context.SaveChangesAsync();

        var query = new GetPublicHomeContentQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.SiteTitle.Should().Be("New Site");
    }

    [Fact]
    public async Task Handle_WithInactiveContent_ShouldIgnoreInactiveContent()
    {
        // Arrange
        var inactiveContent = new HomeContent
        {
            Id = 1,
            SiteTitle = "Inactive Site",
            IsActive = false,
            UpdatedAt = DateTime.UtcNow
        };

        var activeContent = new HomeContent
        {
            Id = 2,
            SiteTitle = "Active Site",
            IsActive = true,
            UpdatedAt = DateTime.UtcNow.AddMinutes(-1)
        };

        _context.HomeContent.AddRange(inactiveContent, activeContent);
        await _context.SaveChangesAsync();

        var query = new GetPublicHomeContentQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.SiteTitle.Should().Be("Active Site");
    }

    [Fact]
    public async Task Handle_WithNoActiveContent_ShouldReturnNull()
    {
        // Arrange
        var inactiveContent = new HomeContent
        {
            Id = 1,
            SiteTitle = "Inactive Site",
            IsActive = false,
            UpdatedAt = DateTime.UtcNow
        };

        _context.HomeContent.Add(inactiveContent);
        await _context.SaveChangesAsync();

        var query = new GetPublicHomeContentQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_WithEmptyDatabase_ShouldReturnNull()
    {
        // Arrange
        var query = new GetPublicHomeContentQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_ShouldLogInformation()
    {
        // Arrange
        var query = new GetPublicHomeContentQuery();

        // Act
        await _handler.Handle(query, CancellationToken.None);

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Fetching public home content")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithNoContent_ShouldLogWarning()
    {
        // Arrange
        var query = new GetPublicHomeContentQuery();

        // Act
        await _handler.Handle(query, CancellationToken.None);

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("No active public home content found")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}