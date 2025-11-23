using GenealogyBlazorApp.Shared.DTOs;
using GenealogyBlazorApp.Shared.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GenealogyBlazorApp.Client.Services;

public class AdminHomeService : IAdminHomeService
{
    private readonly HttpClient _httpClient;

    public AdminHomeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task UpdateHomeContentAsync(UpdateHomeContentRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync("api/admin/home", request);
        response.EnsureSuccessStatusCode();
    }
}
