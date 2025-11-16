using MediatR;

namespace GenealogyBlazorApp.Features.Authentication.Commands;

// Login Command
public record LoginCommand(string Username, string Password) : IRequest<LoginResult>;

public record LoginResult(bool Success, string? SessionToken = null, string? ErrorMessage = null);

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IAuthenticationService _authService;

    public LoginCommandHandler(IAuthenticationService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _authService.AuthenticateAsync(request.Username, request.Password);
            return result;
        }
        catch (Exception ex)
        {
            return new LoginResult(false, ErrorMessage: "Authentication failed");
        }
    }
}

// Logout Command
public record LogoutCommand(string? SessionToken = null) : IRequest<LogoutResult>;

public record LogoutResult(bool Success);

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, LogoutResult>
{
    private readonly IAuthenticationService _authService;

    public LogoutCommandHandler(IAuthenticationService authService)
    {
        _authService = authService;
    }

    public async Task<LogoutResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _authService.SignOutAsync();
        return new LogoutResult(true);
    }
}