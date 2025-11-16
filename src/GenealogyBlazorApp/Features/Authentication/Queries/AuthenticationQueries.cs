using GenealogyBlazorApp.Shared.Features.Authentication.Contracts;
using GenealogyBlazorApp.Features.Authentication.Services;
using MediatR;

namespace GenealogyBlazorApp.Features.Authentication.Queries;

public record GetAuthStatusQuery : IRequest<AuthStatusResponse>;

public class GetAuthStatusQueryHandler : IRequestHandler<GetAuthStatusQuery, AuthStatusResponse>
{
    private readonly IAuthenticationDomainService _authService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAuthStatusQueryHandler(IAuthenticationDomainService authService, IHttpContextAccessor httpContextAccessor)
    {
        _authService = authService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthStatusResponse> Handle(GetAuthStatusQuery request, CancellationToken cancellationToken)
    {
        var isAuthenticated = await _authService.IsAuthenticatedAsync();
        
        if (!isAuthenticated)
        {
            return new AuthStatusResponse(false, false);
        }

        var httpContext = _httpContextAccessor.HttpContext;
        var isAdmin = httpContext?.User?.HasClaim("admin", "true") == true;
        var username = httpContext?.User?.Identity?.Name;

        return new AuthStatusResponse(isAuthenticated, isAdmin, username);
    }
}