using GenealogyBlazorApp.Features.Authentication.Commands;
using GenealogyBlazorApp.Shared.Features.Authentication.Contracts;
using GenealogyBlazorApp.Features.Authentication.Queries;
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
        var command = new LoginCommand(request.Username, request.Password);
        var result = await mediator.Send(command);

        if (result.Success)
        {
            return Results.Ok(result);
        }

        return Results.Unauthorized();
    }

    private static async Task<IResult> LogoutAsync(IMediator mediator)
    {
        var command = new LogoutCommand();
        var result = await mediator.Send(command);

        return Results.Ok(result);
    }

    private static async Task<IResult> GetAuthStatusAsync(IMediator mediator)
    {
        var query = new GetAuthStatusQuery();
        var result = await mediator.Send(query);

        return Results.Ok(result);
    }
}