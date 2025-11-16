namespace GenealogyBlazorApp.Features.Authentication.Contracts;

public record LoginRequest(string Username, string Password);

public record LoginResponse(
    bool Success,
    string? ErrorMessage = null,
    string? Username = null);

public record LogoutResponse(bool Success);

public record AuthStatusResponse(
    bool IsAuthenticated,
    bool IsAdmin,
    string? Username = null);