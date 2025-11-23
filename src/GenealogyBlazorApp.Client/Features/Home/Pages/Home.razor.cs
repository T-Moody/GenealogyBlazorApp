using GenealogyBlazorApp.Client.Features.Home.Models;
using GenealogyBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace GenealogyBlazorApp.Client.Features.Home.Pages;

public partial class Home
{
    [Inject]
    private IPublicHomeService PublicHomeService { get; set; } = default!;

    protected HomeState State { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var content = await PublicHomeService.GetPublicHomeContentAsync();
        if (content is not null)
        {
            State.FromDto(content);
            State.IsLoading = false;
        }
    }
}
