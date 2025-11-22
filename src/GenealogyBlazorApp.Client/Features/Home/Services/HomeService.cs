using GenealogyBlazorApp.Shared.DTOs;
using GenealogyBlazorApp.Shared.Models;
using GenealogyBlazorApp.Shared.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GenealogyBlazorApp.Client.Features.Home.Services
{
    public class HomeService : IHomeService
    {
        private readonly HttpClient _httpClient;

        public HomeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HomeContentDto?> GetHomeContentAsync()
        {
            return await _httpClient.GetFromJsonAsync<HomeContentDto>("api/home-content/admin");
        }

        public async Task<ApiResult> UpdateHomeContentAsync(HomeContentDto homeContent)
        {
            var response = await _httpClient.PutAsJsonAsync("api/home-content/admin", homeContent);
            if (response.IsSuccessStatusCode)
            {
                return new ApiResult { Succeeded = true };
            }
            // In a real app, you'd deserialize the error response here
            return new ApiResult { Succeeded = false, Errors = new[] { "Failed to update content." } };
        }
    }
}
