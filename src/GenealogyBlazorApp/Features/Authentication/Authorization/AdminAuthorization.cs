using Microsoft.AspNetCore.Authorization;

namespace GenealogyBlazorApp.Features.Authentication.Authorization;

public static class AuthorizationPolicies
{
    public const string AdminOnly = "AdminOnly";
}

public class AdminRequirement : IAuthorizationRequirement
{
}

public class AdminAuthorizationHandler : AuthorizationHandler<AdminRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        if (context.User.Identity?.IsAuthenticated == true && 
            context.User.HasClaim("admin", "true"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}