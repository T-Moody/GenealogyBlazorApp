using Microsoft.AspNetCore.Components;

namespace GenealogyBlazorApp.Client.Features.Authentication.Models;

public class ClientLoginState
{
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsAuthenticated { get; set; }
    public string? Username { get; set; }
}