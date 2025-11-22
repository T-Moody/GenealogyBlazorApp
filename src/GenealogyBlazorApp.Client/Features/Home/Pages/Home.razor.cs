using GenealogyBlazorApp.Shared.DTOs;
using GenealogyBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GenealogyBlazorApp.Client.Features.Home.Pages
{
    public partial class Home
    {
        [Inject]
        private IPublicHomeService PublicHomeService { get; set; } = default!;

        private PublicHomeContentDto? content;

        protected override async Task OnInitializedAsync()
        {
            content = await PublicHomeService.GetPublicHomeContentAsync();
        }
    }
}
