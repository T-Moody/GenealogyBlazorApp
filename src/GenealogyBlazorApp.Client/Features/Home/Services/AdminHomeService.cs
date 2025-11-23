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

    public async Task<HomeContentDto?> GetHomeContentAsync()
    {
        var response = await _httpClient.GetAsync("api/home-content/admin");
        
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error getting home content: {response.StatusCode} - {error}");
            throw new HttpRequestException($"Error getting home content: {response.StatusCode} - {error}");
        }

        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<HomeContentDto>();
    }

    public async Task UpdateHomeContentAsync(UpdateHomeContentRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync("api/home-content/admin", request);
        response.EnsureSuccessStatusCode();
    }
}
