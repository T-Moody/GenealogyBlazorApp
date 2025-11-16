using GenealogyBlazorApp.Features.Authentication.Commands;
using GenealogyBlazorApp.Features.Authentication.Queries;
using GenealogyBlazorApp.Shared.Features.Authentication.Contracts;
using GenealogyBlazorApp.Shared.Features.Authentication.Services;
using MediatR;

namespace GenealogyBlazorApp.Features.Authentication.Services;

/// <summary>
/// Server-side implementation of IAuthenticationService that uses MediatR to call domain services directly.
/// Used when running in Server Interactive mode.
/// </summary>
public class ServerAuthenticationService : IAuthenticationService
{
    private readonly IMediator _mediator;

    public ServerAuthenticationService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var command = new LoginCommand(request.Username, request.Password);
        return await _mediator.Send(command);
    }

    public async Task<LogoutResponse> LogoutAsync()
    {
        var command = new LogoutCommand();
        return await _mediator.Send(command);
    }

    public async Task<AuthStatusResponse> GetAuthStatusAsync()
    {
        var query = new GetAuthStatusQuery();
        return await _mediator.Send(query);
    }
}