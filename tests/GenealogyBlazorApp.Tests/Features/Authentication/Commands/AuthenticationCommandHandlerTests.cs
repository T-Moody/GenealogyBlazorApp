using FluentAssertions;
using GenealogyBlazorApp.Features.Authentication.Commands;
using GenealogyBlazorApp.Features.Authentication.Services;
using GenealogyBlazorApp.Shared.Features.Authentication.Contracts;
using Moq;
using Xunit;

namespace GenealogyBlazorApp.Tests.Features.Authentication.Commands;

public class LoginCommandHandlerTests
{
    private readonly Mock<IAuthenticationDomainService> _mockAuthService;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        _mockAuthService = new Mock<IAuthenticationDomainService>();
        _handler = new LoginCommandHandler(_mockAuthService.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldReturnSuccessResponse()
    {
        // Arrange
        var command = new LoginCommand("admin", "password123");
        var expectedResponse = new LoginResponse(true, Username: "admin");
        
        _mockAuthService.Setup(x => x.AuthenticateAsync("admin", "password123"))
            .ReturnsAsync(expectedResponse);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Username.Should().Be("admin");
        result.ErrorMessage.Should().BeNull();
        
        _mockAuthService.Verify(x => x.AuthenticateAsync("admin", "password123"), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidCredentials_ShouldReturnFailureResponse()
    {
        // Arrange
        var command = new LoginCommand("admin", "wrongpassword");
        var expectedResponse = new LoginResponse(false, "Invalid username or password");
        
        _mockAuthService.Setup(x => x.AuthenticateAsync("admin", "wrongpassword"))
            .ReturnsAsync(expectedResponse);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Username.Should().BeNull();
        result.ErrorMessage.Should().Be("Invalid username or password");
    }

    [Theory]
    [InlineData("", "password")]
    [InlineData("username", "")]
    [InlineData(null, "password")]
    [InlineData("username", null)]
    public async Task Handle_WithEmptyCredentials_ShouldReturnValidationError(string username, string password)
    {
        // Arrange
        var command = new LoginCommand(username, password);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Be("Username and password are required");
        
        _mockAuthService.Verify(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenServiceThrowsException_ShouldReturnErrorResponse()
    {
        // Arrange
        var command = new LoginCommand("admin", "password123");
        
        _mockAuthService.Setup(x => x.AuthenticateAsync("admin", "password123"))
            .ThrowsAsync(new Exception("Database connection failed"));
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Be("An error occurred during authentication");
    }
}

public class LogoutCommandHandlerTests
{
    private readonly Mock<IAuthenticationDomainService> _mockAuthService;
    private readonly LogoutCommandHandler _handler;

    public LogoutCommandHandlerTests()
    {
        _mockAuthService = new Mock<IAuthenticationDomainService>();
        _handler = new LogoutCommandHandler(_mockAuthService.Object);
    }

    [Fact]
    public async Task Handle_ShouldCallSignOutAndReturnSuccess()
    {
        // Arrange
        var command = new LogoutCommand();
        
        _mockAuthService.Setup(x => x.SignOutAsync())
            .Returns(Task.CompletedTask);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        
        _mockAuthService.Verify(x => x.SignOutAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenSignOutThrowsException_ShouldReturnFailure()
    {
        // Arrange
        var command = new LogoutCommand();
        
        _mockAuthService.Setup(x => x.SignOutAsync())
            .ThrowsAsync(new Exception("SignOut failed"));
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
    }
}