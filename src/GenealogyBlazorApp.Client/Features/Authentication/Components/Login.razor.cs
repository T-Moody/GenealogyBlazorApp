using GenealogyBlazorApp.Client.Features.Authentication.Models;
using GenealogyBlazorApp.Shared.Features.Authentication.Contracts;
using GenealogyBlazorApp.Shared.Features.Authentication.Services;
using Microsoft.AspNetCore.Components;

namespace GenealogyBlazorApp.Client.Features.Authentication.Components;

public class LoginBase : ComponentBase, IDisposable
{
    [Inject] protected IAuthenticationService AuthService { get; set; } = default!;
    [Inject] protected NavigationManager Navigation { get; set; } = default!;
    [Inject] protected PersistentComponentState ApplicationState { get; set; } = default!;

    protected ClientLoginState State { get; set; } = new();
    protected LoginFormModel FormModel { get; set; } = new();
    protected string SiteName { get; set; } = "Michigan Genealogy Resources";

    private PersistingComponentStateSubscription _persistingSubscription;

    protected override async Task OnInitializedAsync()
    {
        _persistingSubscription = ApplicationState.RegisterOnPersisting(PersistState);

        if (!ApplicationState.TryTakeFromJson<ClientLoginState>(nameof(ClientLoginState), out var restoredState))
        {
            State = new ClientLoginState();
        }
        else
        {
            State = restoredState ?? new ClientLoginState();
        }

        await CheckExistingAuthenticationAsync();
    }

    protected async Task HandleLoginSubmit()
    {
        await ClearErrorMessageAsync();
        await SetLoadingStateAsync(true);

        try
        {
            var loginRequest = new LoginRequest(FormModel.Username, FormModel.Password);
            var result = await AuthService.LoginAsync(loginRequest);

            if (result.Success)
            {
                await HandleSuccessfulLoginAsync(result.Username);
            }
            else
            {
                await HandleLoginErrorAsync(result.ErrorMessage ?? "Login failed");
            }
        }
        catch (Exception)
        {
            await HandleLoginErrorAsync("An unexpected error occurred");
        }
        finally
        {
            await SetLoadingStateAsync(false);
        }
    }

    private async Task CheckExistingAuthenticationAsync()
    {
        try
        {
            var authStatus = await AuthService.GetAuthStatusAsync();
            
            if (authStatus.IsAuthenticated && authStatus.IsAdmin)
            {
                // Already authenticated, redirect to admin dashboard
                Navigation.NavigateTo("/admin", true);
                return;
            }
        }
        catch (Exception)
        {
            // Ignore errors during auth check - user can still attempt login
        }
    }

    private async Task HandleSuccessfulLoginAsync(string? username)
    {
        State.IsAuthenticated = true;
        State.Username = username;
        State.ErrorMessage = null;

        await InvokeAsync(StateHasChanged);
        
        // Navigate to admin dashboard with force reload to ensure server state sync
        Navigation.NavigateTo("/admin", true);
    }

    private async Task HandleLoginErrorAsync(string errorMessage)
    {
        State.ErrorMessage = errorMessage;
        State.IsAuthenticated = false;
        State.Username = null;

        // Clear password field for security
        FormModel.Password = string.Empty;

        await InvokeAsync(StateHasChanged);
    }

    private async Task SetLoadingStateAsync(bool isLoading)
    {
        State.IsLoading = isLoading;
        await InvokeAsync(StateHasChanged);
    }

    private async Task ClearErrorMessageAsync()
    {
        if (!string.IsNullOrEmpty(State.ErrorMessage))
        {
            State.ErrorMessage = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    private Task PersistState()
    {
        ApplicationState.PersistAsJson(nameof(ClientLoginState), State);
        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _persistingSubscription.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}