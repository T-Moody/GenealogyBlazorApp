using GenealogyBlazorApp.Shared.DTOs;
using GenealogyBlazorApp.Shared.Services;
using System.Net.Http.Json;

namespace GenealogyBlazorApp.Client.Features.Home.Services;

public class AdminHomeService : IAdminHomeService
{
    private readonly HttpClient _httpClient;

    public AdminHomeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task UpdateHomeContentAsync(UpdateHomeContentRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync("api/home-content/admin", request);
        response.EnsureSuccessStatusCode();
    }
}
