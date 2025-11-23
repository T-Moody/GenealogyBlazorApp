using GenealogyBlazorApp.Features.Home.Commands;
using GenealogyBlazorApp.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GenealogyBlazorApp.Features.Home;

public static class AdminHomeEndpoints
{
    public static void MapAdminHomeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/home-content/admin").WithTags("Admin Home Content");

        group.MapGet("", async (IMediator mediator) =>
        {
            try 
            {
                var result = await mediator.Send(new GenealogyBlazorApp.Features.Home.Queries.GetHomeContentQuery());
                return result is not null ? Results.Ok(result) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.ToString(), statusCode: 500);
            }
        })
        .WithName("GetHomeContent")
        .Produces<HomeContentDto>(200)
        .Produces(404);

        group.MapPut("", async ([FromBody] UpdateHomeContentRequest request, IMediator mediator) =>
        {
            await mediator.Send(new UpdateHomeContentCommand(request));
            return Results.NoContent();
        })
        .WithName("UpdateHomeContent")
        .Produces(204);
        //.RequireAuthorization("Admin"); // TODO: Add authorization
    }
}
