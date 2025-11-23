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
    }
}