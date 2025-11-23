using GenealogyBlazorApp.Features.Home.Commands;
using GenealogyBlazorApp.Features.Home.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GenealogyBlazorApp.Features.Home.Endpoints;

public static class HomeEndpoints
{
    public static void MapHomeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/home-content").WithTags("Home Content");

        group.MapGet("/public", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetPublicHomeContentQuery());
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetPublicHomeContent")
        .Produces(200)
        .Produces(404);

        group.MapGet("/admin", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetPublicHomeContentQuery());
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetAdminHomeContent")
        .Produces(200)
        .Produces(404);
        //.RequireAuthorization("Admin"); // TODO: Add authorization

        // This is now handled by AdminHomeEndpoints.cs
        //group.MapPut("/admin", async ([FromBody] UpdateHomeContentCommand command, IMediator mediator) =>
        //{
        //    await mediator.Send(command);
        //    return Results.NoContent();
        //})
        //.WithName("UpdateHomeContent")
        //.Produces(204)
        //.Produces(400);
        //.RequireAuthorization("Admin"); // TODO: Add authorization
    }
}