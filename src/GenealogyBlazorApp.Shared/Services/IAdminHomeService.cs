using GenealogyBlazorApp.Shared.DTOs;

namespace GenealogyBlazorApp.Shared.Services;

public interface IAdminHomeService
{
    Task<HomeContentDto?> GetHomeContentAsync();
    Task UpdateHomeContentAsync(UpdateHomeContentRequest request);
}
