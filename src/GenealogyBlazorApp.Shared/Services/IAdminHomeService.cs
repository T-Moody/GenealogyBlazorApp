using GenealogyBlazorApp.Shared.DTOs;

namespace GenealogyBlazorApp.Shared.Services;

public interface IAdminHomeService
{
    Task UpdateHomeContentAsync(UpdateHomeContentRequest request);
}
