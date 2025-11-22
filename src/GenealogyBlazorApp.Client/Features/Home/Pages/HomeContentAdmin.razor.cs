using GenealogyBlazorApp.Shared.DTOs;
using GenealogyBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GenealogyBlazorApp.Client.Features.Home.Pages
{
    public partial class HomeContentAdmin
    {
        private HomeContentDto? homeContent;

        [Inject]
        private IHomeService HomeService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            homeContent = await HomeService.GetHomeContentAsync();
        }

        private async Task HandleValidSubmit()
        {
            if (homeContent is not null)
            {
                await HomeService.UpdateHomeContentAsync(homeContent);
            }
        }
    }
}
