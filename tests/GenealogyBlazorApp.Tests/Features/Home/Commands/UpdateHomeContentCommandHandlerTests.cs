using FluentAssertions;
using GenealogyBlazorApp.Core.Shared;
using GenealogyBlazorApp.Features.Home.Commands;
using GenealogyBlazorApp.Infrastructure.Data;
using GenealogyBlazorApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GenealogyBlazorApp.Tests.Features.Home.Commands;

public class UpdateHomeContentCommandHandlerTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly Mock<ILogger<UpdateHomeContentCommandHandler>> _mockLogger;
    private readonly UpdateHomeContentCommandHandler _handler;

    public UpdateHomeContentCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _mockLogger = new Mock<ILogger<UpdateHomeContentCommandHandler>>();
        _handler = new UpdateHomeContentCommandHandler(_context, _mockLogger.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldUpdateHomeContentSuccessfully()
    {
        // Arrange
        var existingContent = new HomeContent
        {
            Id = 1,
            SiteTitle = "Old Title",
            Tagline = "Old Tagline",
            AboutContent = "Old About",
            SidebarLinks = "[]",
            IsActive = true,
            UpdatedAt = DateTime.UtcNow.AddDays(-1)
        };

        _context.HomeContent.Add(existingContent);
        await _context.SaveChangesAsync();

        var command = new UpdateHomeContentCommand
        {
            Id = 1,
            SiteTitle = "New Title",
            Tagline = "New Tagline",
            AboutContent = "New About Content",
            SidebarLinks = "[{\"text\":\"Link\", \"url\":\"#\"}]"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull();

        // Verify database was updated
        var updatedContent = await _context.HomeContent.FindAsync(1);
        updatedContent.Should().NotBeNull();
        updatedContent!.SiteTitle.Should().Be("New Title");
        updatedContent.Tagline.Should().Be("New Tagline");
        updatedContent.AboutContent.Should().Be("New About Content");
        updatedContent.SidebarLinks.Should().Be("[{\"text\":\"Link\", \"url\":\"#\"}]");
    }

    [Fact]
    public async Task Handle_WithNonExistentId_ShouldReturnFailure()
    {
        // Arrange
        var command = new UpdateHomeContentCommand
        {
            Id = 999,
            SiteTitle = "New Title",
            Tagline = "New Tagline",
            AboutContent = "New About Content",
            SidebarLinks = "[]"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Home content with ID: 999 not found.");
    }

    [Fact]
    public async Task Handle_WithDatabaseException_ShouldReturnFailure()
    {
        // Arrange
        var existingContent = new HomeContent
        {
            Id = 1,
            SiteTitle = "Old Title",
            IsActive = true
        };

        _context.HomeContent.Add(existingContent);
        await _context.SaveChangesAsync();

        // Dispose the context to simulate a database error
        await _context.DisposeAsync();

        var command = new UpdateHomeContentCommand
        {
            Id = 1,
            SiteTitle = "New Title",
            Tagline = "New Tagline",
            AboutContent = "New About Content",
            SidebarLinks = "[]"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().StartWith("An error occurred while updating home content:");
    }

    [Theory]
    [InlineData("", "Tagline", "About", "[]")]
    [InlineData("Title", "", "About", "[]")]
    [InlineData("Title", "Tagline", "", "[]")]
    public async Task Handle_WithValidEmptyFields_ShouldUpdateSuccessfully(
        string siteTitle, string tagline, string aboutContent, string sidebarLinks)
    {
        // Arrange
        var existingContent = new HomeContent
        {
            Id = 1,
            SiteTitle = "Old Title",
            Tagline = "Old Tagline",
            AboutContent = "Old About",
            SidebarLinks = "[]",
            IsActive = true
        };

        _context.HomeContent.Add(existingContent);
        await _context.SaveChangesAsync();

        var command = new UpdateHomeContentCommand
        {
            Id = 1,
            SiteTitle = siteTitle,
            Tagline = tagline,
            AboutContent = aboutContent,
            SidebarLinks = sidebarLinks
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        
        var updatedContent = await _context.HomeContent.FindAsync(1);
        updatedContent!.SiteTitle.Should().Be(siteTitle);
        updatedContent.Tagline.Should().Be(tagline);
        updatedContent.AboutContent.Should().Be(aboutContent);
        updatedContent.SidebarLinks.Should().Be(sidebarLinks);
    }

    [Fact]
    public async Task Handle_ShouldLogInformationOnStart()
    {
        // Arrange
        var existingContent = new HomeContent
        {
            Id = 1,
            SiteTitle = "Test Title",
            IsActive = true
        };

        _context.HomeContent.Add(existingContent);
        await _context.SaveChangesAsync();

        var command = new UpdateHomeContentCommand
        {
            Id = 1,
            SiteTitle = "New Title",
            Tagline = "New Tagline",
            AboutContent = "New About",
            SidebarLinks = "[]"
        };

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Updating home content for ID: 1")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldLogSuccessOnCompletion()
    {
        // Arrange
        var existingContent = new HomeContent
        {
            Id = 1,
            SiteTitle = "Test Title",
            IsActive = true
        };

        _context.HomeContent.Add(existingContent);
        await _context.SaveChangesAsync();

        var command = new UpdateHomeContentCommand
        {
            Id = 1,
            SiteTitle = "New Title",
            Tagline = "New Tagline",
            AboutContent = "New About",
            SidebarLinks = "[]"
        };

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Successfully updated home content for ID: 1")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistentId_ShouldLogWarning()
    {
        // Arrange
        var command = new UpdateHomeContentCommand
        {
            Id = 999,
            SiteTitle = "New Title",
            Tagline = "New Tagline",
            AboutContent = "New About",
            SidebarLinks = "[]"
        };

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Home content with ID: 999 not found")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithException_ShouldLogError()
    {
        // Arrange
        var existingContent = new HomeContent
        {
            Id = 1,
            SiteTitle = "Test Title",
            IsActive = true
        };

        _context.HomeContent.Add(existingContent);
        await _context.SaveChangesAsync();

        // Dispose the context to simulate a database error
        await _context.DisposeAsync();

        var command = new UpdateHomeContentCommand
        {
            Id = 1,
            SiteTitle = "New Title",
            Tagline = "New Tagline",
            AboutContent = "New About",
            SidebarLinks = "[]"
        };

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Error updating home content for ID: 1")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}