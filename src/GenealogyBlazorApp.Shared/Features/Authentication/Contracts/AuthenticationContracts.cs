using System.ComponentModel.DataAnnotations;

namespace GenealogyBlazorApp.Shared.Features.Authentication.Contracts;

// Shared DTOs for API communication
public record LoginRequest(
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, ErrorMessage = "Username must be less than 50 characters")]
    string Username,
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
    string Password);

public record LoginResponse(
    bool Success,
    string? ErrorMessage = null,
    string? Username = null);

public record LogoutResponse(bool Success);

public record AuthStatusResponse(
    bool IsAuthenticated,
    bool IsAdmin,
    string? Username = null);

// Shared form model for validation
public class LoginFormModel
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, ErrorMessage = "Username must be less than 50 characters")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
    public string Password { get; set; } = string.Empty;
}