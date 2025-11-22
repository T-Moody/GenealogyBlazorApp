using GenealogyBlazorApp.Shared.DTOs;

namespace GenealogyBlazorApp.Shared.Services;

public interface IPublicHomeService
{
    Task<PublicHomeContentDto?> GetPublicHomeContentAsync();
}
