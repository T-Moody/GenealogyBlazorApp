using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace GenealogyBlazorApp.Features.Authentication.Models;

[PersistentComponentState]
public class LoginState
{
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsAuthenticated { get; set; }
    public string? Username { get; set; }
}

public class LoginFormModel
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, ErrorMessage = "Username must be less than 50 characters")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
    public string Password { get; set; } = string.Empty;
}