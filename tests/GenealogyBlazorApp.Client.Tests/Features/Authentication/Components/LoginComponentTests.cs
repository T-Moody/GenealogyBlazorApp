using Bunit;
using FluentAssertions;
using GenealogyBlazorApp.Client.Features.Authentication.Components;
using GenealogyBlazorApp.Shared.Features.Authentication.Contracts;
using GenealogyBlazorApp.Shared.Features.Authentication.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace GenealogyBlazorApp.Client.Tests.Features.Authentication.Components;

public class LoginComponentTests : TestContext
{
    private readonly Mock<IAuthenticationService> _mockAuthService;
    private readonly Mock<NavigationManager> _mockNavigationManager;

    public LoginComponentTests()
    {
        _mockAuthService = new Mock<IAuthenticationService>();
        _mockNavigationManager = new Mock<NavigationManager>();
        
        // Register services
        Services.AddSingleton(_mockAuthService.Object);
        Services.AddSingleton(_mockNavigationManager.Object);
    }

    [Fact]
    public void Login_ShouldRenderCorrectly()
    {
        // Act
        var component = RenderComponent<Login>();
        
        // Assert
        component.Should().NotBeNull();
        
        // Check for page title
        component.Find("title").TextContent.Should().Contain("Admin Login");
        
        // Check for form elements
        component.Find("form").Should().NotBeNull();
        component.Find("input[id='username']").Should().NotBeNull();
        component.Find("input[id='password'][type='password']").Should().NotBeNull();
        component.Find("button[type='submit']").Should().NotBeNull();
        
        // Check for Bootstrap styling
        component.Find(".card").Should().NotBeNull();
        component.Find(".card-header.bg-primary").Should().NotBeNull();
        component.Find(".card-body").Should().NotBeNull();
        
        // Check for accessibility attributes
        var usernameInput = component.Find("input[id='username']");
        usernameInput.GetAttribute("autocomplete").Should().Be("username");
        
        var passwordInput = component.Find("input[id='password']");
        passwordInput.GetAttribute("autocomplete").Should().Be("current-password");
    }

    [Fact]
    public void Login_InitialState_ShouldHaveCorrectDefaults()
    {
        // Act
        var component = RenderComponent<Login>();
        
        // Assert
        var usernameInput = component.Find("input[id='username']");
        var passwordInput = component.Find("input[id='password']");
        var submitButton = component.Find("button[type='submit']");
        
        usernameInput.GetAttribute("value").Should().BeEmpty();
        passwordInput.GetAttribute("value").Should().BeEmpty();
        submitButton.GetAttribute("disabled").Should().BeNull();
        
        // Should not show error message initially
        component.FindAll(".alert-danger").Should().BeEmpty();
    }

    [Fact]
    public async Task Login_WithValidCredentials_ShouldCallAuthServiceAndNavigate()
    {
        // Arrange
        var expectedResponse = new LoginResponse(true, Username: "admin");
        _mockAuthService.Setup(x => x.LoginAsync(It.IsAny<LoginRequest>()))
            .ReturnsAsync(expectedResponse);
        
        var component = RenderComponent<Login>();
        
        // Act
        var usernameInput = component.Find("input[id='username']");
        var passwordInput = component.Find("input[id='password']");
        var form = component.Find("form");
        
        await usernameInput.ChangeAsync(new Microsoft.AspNetCore.Components.ChangeEventArgs
        {
            Value = "admin"
        });
        
        await passwordInput.ChangeAsync(new Microsoft.AspNetCore.Components.ChangeEventArgs
        {
            Value = "admin123"
        });
        
        await form.SubmitAsync();
        
        // Assert
        _mockAuthService.Verify(x => x.LoginAsync(It.Is<LoginRequest>(r => 
            r.Username == "admin" && r.Password == "admin123")), Times.Once);
        
        _mockNavigationManager.Verify(x => x.NavigateTo("/admin", true), Times.Once);
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ShouldShowErrorMessage()
    {
        // Arrange
        var errorResponse = new LoginResponse(false, "Invalid username or password");
        _mockAuthService.Setup(x => x.LoginAsync(It.IsAny<LoginRequest>()))
            .ReturnsAsync(errorResponse);
        
        var component = RenderComponent<Login>();
        
        // Act
        var usernameInput = component.Find("input[id='username']");
        var passwordInput = component.Find("input[id='password']");
        var form = component.Find("form");
        
        await usernameInput.ChangeAsync(new Microsoft.AspNetCore.Components.ChangeEventArgs
        {
            Value = "admin"
        });
        
        await passwordInput.ChangeAsync(new Microsoft.AspNetCore.Components.ChangeEventArgs
        {
            Value = "wrongpassword"
        });
        
        await form.SubmitAsync();
        
        // Assert
        var errorAlert = component.Find(".alert-danger");
        errorAlert.Should().NotBeNull();
        errorAlert.TextContent.Should().Contain("Invalid username or password");
        
        // Password field should be cleared for security
        passwordInput.GetAttribute("value").Should().BeEmpty();
    }

    [Fact]
    public async Task Login_DuringSubmission_ShouldShowLoadingState()
    {
        // Arrange
        var taskCompletionSource = new TaskCompletionSource<LoginResponse>();
        _mockAuthService.Setup(x => x.LoginAsync(It.IsAny<LoginRequest>()))
            .Returns(taskCompletionSource.Task);
        
        var component = RenderComponent<Login>();
        
        // Act
        var usernameInput = component.Find("input[id='username']");
        var passwordInput = component.Find("input[id='password']");
        var form = component.Find("form");
        
        await usernameInput.ChangeAsync(new Microsoft.AspNetCore.Components.ChangeEventArgs
        {
            Value = "admin"
        });
        
        await passwordInput.ChangeAsync(new Microsoft.AspNetCore.Components.ChangeEventArgs
        {
            Value = "admin123"
        });
        
        // Start form submission (but don't complete the task yet)
        var submitTask = form.SubmitAsync();
        
        // Assert loading state
        var submitButton = component.Find("button[type='submit']");
        submitButton.GetAttribute("disabled").Should().NotBeNull();
        
        var spinner = component.Find(".spinner-border");
        spinner.Should().NotBeNull();
        
        component.Find("button").TextContent.Should().Contain("Signing in...");
        
        // Complete the task
        taskCompletionSource.SetResult(new LoginResponse(true, Username: "admin"));
        await submitTask;
    }

    [Fact]
    public void Login_FormValidation_ShouldShowValidationMessages()
    {
        // Arrange
        var component = RenderComponent<Login>();
        
        // Act - Try to submit form with empty fields
        var form = component.Find("form");
        form.Submit();
        
        // Assert
        var validationMessages = component.FindAll(".text-danger.small");
        validationMessages.Should().HaveCount(2); // Username and password validation
        
        validationMessages.Should().Contain(vm => vm.TextContent.Contains("Username is required"));
        validationMessages.Should().Contain(vm => vm.TextContent.Contains("Password is required"));
    }

    [Fact]
    public async Task Login_WithAuthenticatedUser_ShouldRedirectToAdmin()
    {
        // Arrange
        var authStatusResponse = new AuthStatusResponse(true, true, "admin");
        _mockAuthService.Setup(x => x.GetAuthStatusAsync())
            .ReturnsAsync(authStatusResponse);
        
        // Act
        var component = RenderComponent<Login>();
        
        // Assert
        _mockNavigationManager.Verify(x => x.NavigateTo("/admin", true), Times.Once);
    }
}