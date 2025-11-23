using GenealogyBlazorApp.Client.Features.Home.Models;
using GenealogyBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using GenealogyBlazorApp.Shared.DTOs;
using System.Text.Json;

namespace GenealogyBlazorApp.Client.Features.Home.Pages;

public partial class Editor
{
    [Inject]
    private IAdminHomeService AdminHomeService { get; set; } = default!;

    protected HomeState State { get; set; } = new();
    
    protected bool IsSaving { get; set; }
    protected string? SaveMessage { get; set; }
    protected bool IsError { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var content = await AdminHomeService.GetHomeContentAsync();
        if (content is not null)
        {
            State.FromDto(content);
            State.IsLoading = false;
        }
    }

    private async Task SaveChanges()
    {
        IsSaving = true;
        SaveMessage = "Saving changes...";
        IsError = false;
        StateHasChanged();

        try
        {
            var request = new UpdateHomeContentRequest
            {
                SiteTitle = State.SiteTitle,
                Tagline = State.Tagline,
                AboutContent = State.AboutContent,
                AboutSectionTitle = State.AboutSectionTitle,
                HeroImagePath = State.HeroImagePath,
                ProfileImagePath = State.ProfileImagePath,
                ProfileImageCaption = State.ProfileImageCaption,
                SidebarLinks = JsonSerializer.Serialize(State.Links),
                
                // County Cards
                HuronTitle = State.HuronTitle,
                HuronImagePath = State.HuronImagePath,
                SanilacTitle = State.SanilacTitle,
                SanilacImagePath = State.SanilacImagePath,
                TuscolaTitle = State.TuscolaTitle,
                TuscolaImagePath = State.TuscolaImagePath
            };

            await AdminHomeService.UpdateHomeContentAsync(request);
            SaveMessage = "Changes saved successfully.";
        }
        catch (Exception)
        {
            IsError = true;
            SaveMessage = "Failed to save changes. Please try again.";
        }
        finally
        {
            IsSaving = false;
            StateHasChanged();
            
            // Clear success message after a delay
            if (!IsError)
            {
                await Task.Delay(3000);
                SaveMessage = null;
                StateHasChanged();
            }
        }
    }
}
