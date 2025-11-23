using GenealogyBlazorApp.Client.Features.Home.Models;
using Microsoft.AspNetCore.Components;

namespace GenealogyBlazorApp.Client.Features.Home.Components;

public partial class AboutSection
{
    [Parameter, EditorRequired]
    public HomeState State { get; set; } = new();

    [Parameter]
    public bool IsEditor { get; set; }
}
