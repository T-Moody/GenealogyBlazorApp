using GenealogyBlazorApp.Shared.DTOs;
using GenealogyBlazorApp.Shared.Models;

namespace GenealogyBlazorApp.Shared.Services;

public interface IHomeService
{
    Task<HomeContentDto?> GetHomeContentAsync();
    Task<ApiResult> UpdateHomeContentAsync(HomeContentDto content);
}
