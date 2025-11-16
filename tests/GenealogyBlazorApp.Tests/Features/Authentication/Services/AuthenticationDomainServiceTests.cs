using FluentAssertions;
using GenealogyBlazorApp.Domain.Entities;
using GenealogyBlazorApp.Features.Authentication.Services;
using GenealogyBlazorApp.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;
using Xunit;

namespace GenealogyBlazorApp.Tests.Features.Authentication.Services;

public class AuthenticationDomainServiceTests : IDisposable
{
    private readonly GenealogyDbContext _dbContext;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<HttpContext> _mockHttpContext;
    private readonly AuthenticationDomainService _authService;

    public AuthenticationDomainServiceTests()
    {
        // Setup in-memory database
        var options = new DbContextOptionsBuilder<GenealogyDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _dbContext = new GenealogyDbContext(options);
        
        // Setup mocks
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockHttpContext = new Mock<HttpContext>();
        
        _mockHttpContextAccessor.Setup(x => x.HttpContext)
            .Returns(_mockHttpContext.Object);
        
        _authService = new AuthenticationDomainService(_dbContext, _mockHttpContextAccessor.Object);
        
        // Seed test data
        SeedTestData();
    }

    [Fact]
    public async Task AuthenticateAsync_WithValidCredentials_ShouldReturnSuccessResult()
    {
        // Arrange
        var username = "admin";
        var password = "admin123";
        
        // Act
        var result = await _authService.AuthenticateAsync(username, password);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Username.Should().Be(username);
        result.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public async Task AuthenticateAsync_WithInvalidUsername_ShouldReturnFailureResult()
    {
        // Arrange
        var username = "nonexistent";
        var password = "admin123";
        
        // Act
        var result = await _authService.AuthenticateAsync(username, password);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Username.Should().BeNull();
        result.ErrorMessage.Should().Be("Invalid username or password");
    }

    [Fact]
    public async Task AuthenticateAsync_WithInvalidPassword_ShouldReturnFailureResult()
    {
        // Arrange
        var username = "admin";
        var password = "wrongpassword";
        
        // Act
        var result = await _authService.AuthenticateAsync(username, password);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Username.Should().BeNull();
        result.ErrorMessage.Should().Be("Invalid username or password");
    }

    [Fact]
    public async Task AuthenticateAsync_WithInactiveUser_ShouldReturnFailureResult()
    {
        // Arrange
        var inactiveUser = new AdminUser
        {
            Id = 2,
            Username = "inactive",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
            IsActive = false,
            LastLoginDate = DateTime.UtcNow
        };
        
        _dbContext.AdminUsers.Add(inactiveUser);
        await _dbContext.SaveChangesAsync();
        
        // Act
        var result = await _authService.AuthenticateAsync("inactive", "password123");
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Be("Invalid username or password");
    }

    [Theory]
    [InlineData("", "password")]
    [InlineData("username", "")]
    [InlineData(null, "password")]
    [InlineData("username", null)]
    public async Task AuthenticateAsync_WithNullOrEmptyCredentials_ShouldReturnFailureResult(string username, string password)
    {
        // Act
        var result = await _authService.AuthenticateAsync(username, password);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Contain("Invalid username or password");
    }

    [Fact]
    public async Task AuthenticateAsync_SuccessfulLogin_ShouldUpdateLastLoginDate()
    {
        // Arrange
        var username = "admin";
        var password = "admin123";
        var initialLoginDate = DateTime.UtcNow.AddDays(-1);
        
        var user = await _dbContext.AdminUsers.FirstAsync(u => u.Username == username);
        user.LastLoginDate = initialLoginDate;
        await _dbContext.SaveChangesAsync();
        
        // Act
        await _authService.AuthenticateAsync(username, password);
        
        // Assert
        var updatedUser = await _dbContext.AdminUsers.FirstAsync(u => u.Username == username);
        updatedUser.LastLoginDate.Should().BeAfter(initialLoginDate);
    }

    [Fact]
    public async Task IsAuthenticatedAsync_WithAuthenticatedUser_ShouldReturnTrue()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "admin"),
            new(ClaimTypes.NameIdentifier, "1"),
            new("admin", "true")
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);
        
        _mockHttpContext.Setup(x => x.User).Returns(principal);
        
        // Act
        var result = await _authService.IsAuthenticatedAsync();
        
        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task IsAuthenticatedAsync_WithUnauthenticatedUser_ShouldReturnFalse()
    {
        // Arrange
        var identity = new ClaimsIdentity();
        var principal = new ClaimsPrincipal(identity);
        
        _mockHttpContext.Setup(x => x.User).Returns(principal);
        
        // Act
        var result = await _authService.IsAuthenticatedAsync();
        
        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task SignOutAsync_ShouldCallSignOutOnHttpContext()
    {
        // Arrange
        _mockHttpContext.Setup(x => x.SignOutAsync(It.IsAny<string>()))
            .Returns(Task.CompletedTask)
            .Verifiable();
        
        // Act
        await _authService.SignOutAsync();
        
        // Assert
        _mockHttpContext.Verify(x => x.SignOutAsync("Cookies"), Times.Once);
    }

    private void SeedTestData()
    {
        var adminUser = new AdminUser
        {
            Id = 1,
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            IsActive = true,
            LastLoginDate = DateTime.UtcNow
        };
        
        _dbContext.AdminUsers.Add(adminUser);
        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}