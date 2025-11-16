using GenealogyBlazorApp.Shared.Features.Authentication.Contracts;
using GenealogyBlazorApp.Features.Authentication.Services;
using MediatR;

namespace GenealogyBlazorApp.Features.Authentication.Commands;

public record LoginCommand(string Username, string Password) : IRequest<LoginResponse>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IAuthenticationDomainService _authService;

    public LoginCommandHandler(IAuthenticationDomainService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return new LoginResponse(false, "Username and password are required");
        }

        try
        {
            var result = await _authService.AuthenticateAsync(request.Username, request.Password);
            return result;
        }
        catch (Exception)
        {
            return new LoginResponse(false, "An error occurred during authentication");
        }
    }
}

public record LogoutCommand : IRequest<LogoutResponse>;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, LogoutResponse>
{
    private readonly IAuthenticationDomainService _authService;

    public LogoutCommandHandler(IAuthenticationDomainService authService)
    {
        _authService = authService;
    }

    public async Task<LogoutResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _authService.SignOutAsync();
            return new LogoutResponse(true);
        }
        catch (Exception)
        {
            return new LogoutResponse(false);
        }
    }
}