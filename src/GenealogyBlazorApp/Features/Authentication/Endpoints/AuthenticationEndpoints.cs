using GenealogyBlazorApp.Features.Authentication.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace GenealogyBlazorApp.Features.Authentication.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/auth")
            .WithTags("Authentication");

        group.MapPost("/login", LoginAsync)
            .AllowAnonymous()
            .WithName("Login")
            .WithSummary("Authenticate admin user");

        group.MapPost("/logout", LogoutAsync)
            .RequireAuthorization("AdminOnly")
            .WithName("Logout")
            .WithSummary("Sign out admin user");

        group.MapGet("/status", GetAuthStatusAsync)
            .WithName("AuthStatus")
            .WithSummary("Get current authentication status");
    }

    private static async Task<IResult> LoginAsync(LoginRequest request, IMediator mediator)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return Results.BadRequest(new { success = false, message = "Username and password are required" });
        }

        var command = new LoginCommand(request.Username, request.Password);
        var result = await mediator.Send(command);

        if (result.Success)
        {
            return Results.Ok(new { success = true, message = "Login successful" });
        }

        return Results.Unauthorized();
    }

    private static async Task<IResult> LogoutAsync(IMediator mediator)
    {
        var command = new LogoutCommand();
        var result = await mediator.Send(command);

        return Results.Ok(new { success = result.Success, message = "Logout successful" });
    }

    private static Task<IResult> GetAuthStatusAsync(HttpContext context)
    {
        var isAuthenticated = context.User.Identity?.IsAuthenticated == true;
        var isAdmin = context.User.HasClaim("admin", "true");

        return Task.FromResult(Results.Ok(new { 
            isAuthenticated, 
            isAdmin,
            username = context.User.Identity?.Name 
        }));
    }
}

public record LoginRequest(string Username, string Password);