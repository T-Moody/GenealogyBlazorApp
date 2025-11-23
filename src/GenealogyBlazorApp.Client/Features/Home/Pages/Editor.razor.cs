using GenealogyBlazorApp.Client.Features.Home.Models;
using GenealogyBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using GenealogyBlazorApp.Shared.DTOs;

namespace GenealogyBlazorApp.Client.Features.Home.Pages;

public partial class Editor
{
    [Inject]
    private IPublicHomeService PublicHomeService { get; set; } = default!;

    [Inject]
    private IAdminHomeService AdminHomeService { get; set; } = default!;

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

    private async Task SaveChanges()
    {
        var request = new UpdateHomeContentRequest
        {
            SiteTitle = State.SiteTitle,
            Tagline = State.Tagline,
            AboutContent = State.AboutContent,
            HeroImagePath = State.HeroImagePath,
            ProfileImagePath = State.ProfileImagePath,
            SidebarLinks = State.SidebarLinks
        };

        await AdminHomeService.UpdateHomeContentAsync(request);
    }
}
