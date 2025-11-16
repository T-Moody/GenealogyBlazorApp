using FluentAssertions;
using GenealogyBlazorApp.Shared.Features.Authentication.Contracts;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace GenealogyBlazorApp.Shared.Tests.Features.Authentication.Contracts;

public class AuthenticationContractsTests
{
    [Theory]
    [InlineData("admin", "password123", true)]
    [InlineData("user@example.com", "myPassword", true)]
    [InlineData("", "password", false)]
    [InlineData("username", "", false)]
    [InlineData(null, "password", false)]
    [InlineData("username", null, false)]
    [InlineData("a", "12345", false)] // Password too short
    [InlineData("verylongusernamethatexceedsfiftycharacterslimitandshouldfail", "password", false)] // Username too long
    public void LoginRequest_Validation_ShouldValidateCorrectly(string username, string password, bool expectedValid)
    {
        // Arrange
        var loginRequest = new LoginRequest(username, password);
        var validationContext = new ValidationContext(loginRequest);
        var validationResults = new List<ValidationResult>();
        
        // Act
        var isValid = Validator.TryValidateObject(loginRequest, validationContext, validationResults, true);
        
        // Assert
        isValid.Should().Be(expectedValid);
        
        if (!expectedValid)
        {
            validationResults.Should().NotBeEmpty();
        }
    }

    [Fact]
    public void LoginFormModel_Validation_ShouldValidateRequiredFields()
    {
        // Arrange
        var formModel = new LoginFormModel
        {
            Username = "",
            Password = ""
        };
        
        var validationContext = new ValidationContext(formModel);
        var validationResults = new List<ValidationResult>();
        
        // Act
        var isValid = Validator.TryValidateObject(formModel, validationContext, validationResults, true);
        
        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().HaveCount(2);
        
        validationResults.Should().Contain(vr => vr.ErrorMessage == "Username is required");
        validationResults.Should().Contain(vr => vr.ErrorMessage == "Password is required");
    }

    [Fact]
    public void LoginFormModel_Validation_ShouldValidateStringLengths()
    {
        // Arrange
        var formModel = new LoginFormModel
        {
            Username = new string('a', 51), // Too long
            Password = "12345" // Too short
        };
        
        var validationContext = new ValidationContext(formModel);
        var validationResults = new List<ValidationResult>();
        
        // Act
        var isValid = Validator.TryValidateObject(formModel, validationContext, validationResults, true);
        
        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().HaveCount(2);
        
        validationResults.Should().Contain(vr => vr.ErrorMessage!.Contains("Username must be less than 50 characters"));
        validationResults.Should().Contain(vr => vr.ErrorMessage!.Contains("Password must be between 6 and 100 characters"));
    }

    [Fact]
    public void LoginResponse_WithSuccess_ShouldHaveCorrectProperties()
    {
        // Arrange & Act
        var response = new LoginResponse(true, Username: "testuser");
        
        // Assert
        response.Success.Should().BeTrue();
        response.Username.Should().Be("testuser");
        response.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public void LoginResponse_WithFailure_ShouldHaveCorrectProperties()
    {
        // Arrange & Act
        var response = new LoginResponse(false, "Login failed");
        
        // Assert
        response.Success.Should().BeFalse();
        response.Username.Should().BeNull();
        response.ErrorMessage.Should().Be("Login failed");
    }

    [Fact]
    public void LogoutResponse_ShouldHaveCorrectProperties()
    {
        // Arrange & Act
        var successResponse = new LogoutResponse(true);
        var failureResponse = new LogoutResponse(false);
        
        // Assert
        successResponse.Success.Should().BeTrue();
        failureResponse.Success.Should().BeFalse();
    }

    [Theory]
    [InlineData(true, true, "admin")]
    [InlineData(true, false, "user")]
    [InlineData(false, false, null)]
    public void AuthStatusResponse_ShouldHaveCorrectProperties(bool isAuthenticated, bool isAdmin, string? username)
    {
        // Arrange & Act
        var response = new AuthStatusResponse(isAuthenticated, isAdmin, username);
        
        // Assert
        response.IsAuthenticated.Should().Be(isAuthenticated);
        response.IsAdmin.Should().Be(isAdmin);
        response.Username.Should().Be(username);
    }

    [Fact]
    public void LoginRequest_WithValidData_ShouldCreateCorrectly()
    {
        // Arrange & Act
        var request = new LoginRequest("testuser", "testpassword");
        
        // Assert
        request.Username.Should().Be("testuser");
        request.Password.Should().Be("testpassword");
    }

    [Fact]
    public void LoginFormModel_PropertySetters_ShouldWorkCorrectly()
    {
        // Arrange
        var formModel = new LoginFormModel();
        
        // Act
        formModel.Username = "testuser";
        formModel.Password = "testpassword";
        
        // Assert
        formModel.Username.Should().Be("testuser");
        formModel.Password.Should().Be("testpassword");
    }
}