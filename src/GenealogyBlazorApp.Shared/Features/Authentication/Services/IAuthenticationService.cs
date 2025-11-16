using GenealogyBlazorApp.Shared.Features.Authentication.Contracts;

namespace GenealogyBlazorApp.Shared.Features.Authentication.Services;

/// <summary>
/// Authentication service interface that works in both Server Interactive and WASM modes.
/// Server implementation calls domain services directly.
/// WASM implementation makes HTTP API calls.
/// </summary>
public interface IAuthenticationService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LogoutResponse> LogoutAsync();
    Task<AuthStatusResponse> GetAuthStatusAsync();
}

/// <summary>
/// Client state management interface for authentication state across render modes.
/// </summary>
public interface IAuthenticationStateService
{
    Task<AuthStatusResponse> GetCurrentAuthStatusAsync();
    Task SetAuthStatusAsync(AuthStatusResponse authStatus);
    Task ClearAuthStatusAsync();
    event EventHandler<AuthStatusResponse>? AuthStatusChanged;
}