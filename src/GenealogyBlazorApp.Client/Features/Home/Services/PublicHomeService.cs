using GenealogyBlazorApp.Shared.DTOs;
using GenealogyBlazorApp.Shared.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GenealogyBlazorApp.Client.Features.Home.Services
{
    public class PublicHomeService : IPublicHomeService
    {
        private readonly HttpClient _httpClient;

        public PublicHomeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PublicHomeContentDto?> GetPublicHomeContentAsync()
        {
            return await _httpClient.GetFromJsonAsync<PublicHomeContentDto>("api/home-content/public");
        }
    }
}
