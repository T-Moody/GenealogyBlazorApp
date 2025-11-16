using GenealogyBlazorApp.Shared.Features.Authentication.Contracts;
using GenealogyBlazorApp.Shared.Features.Authentication.Services;
using System.Net.Http.Json;

namespace GenealogyBlazorApp.Client.Features.Authentication.Services;

/// <summary>
/// WASM-side implementation of IAuthenticationService that makes HTTP API calls.
/// Used when running in WebAssembly mode.
/// </summary>
public class ClientAuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public ClientAuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", request);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result ?? new LoginResponse(false, "Invalid response from server");
            }

            return new LoginResponse(false, "Login failed");
        }
        catch (Exception)
        {
            return new LoginResponse(false, "Connection error. Please try again.");
        }
    }

    public async Task<LogoutResponse> LogoutAsync()
    {
        try
        {
            var response = await _httpClient.PostAsync("/api/auth/logout", null);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LogoutResponse>();
                return result ?? new LogoutResponse(false);
            }

            return new LogoutResponse(false);
        }
        catch (Exception)
        {
            return new LogoutResponse(false);
        }
    }

    public async Task<AuthStatusResponse> GetAuthStatusAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/auth/status");
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthStatusResponse>();
                return result ?? new AuthStatusResponse(false, false);
            }

            return new AuthStatusResponse(false, false);
        }
        catch (Exception)
        {
            return new AuthStatusResponse(false, false);
        }
    }
}