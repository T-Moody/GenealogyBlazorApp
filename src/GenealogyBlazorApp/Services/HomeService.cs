using GenealogyBlazorApp.Features.Home.Queries;
using GenealogyBlazorApp.Features.Home.Commands;
using GenealogyBlazorApp.Shared.DTOs;
using GenealogyBlazorApp.Shared.Models;
using GenealogyBlazorApp.Shared.Services;
using MediatR;

namespace GenealogyBlazorApp.Services;

public class HomeService : IHomeService
{
    private readonly IMediator _mediator;

    public HomeService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<HomeContentDto?> GetHomeContentAsync()
    {
        return await _mediator.Send(new GetHomeContentQuery());
    }

    public async Task<ApiResult> UpdateHomeContentAsync(HomeContentDto content)
    {
        var command = new UpdateHomeContentCommand
        {
            Id = content.Id,
            SiteTitle = content.SiteTitle,
            Tagline = content.Tagline,
            AboutContent = content.AboutContent,
            SidebarLinks = content.SidebarLinks
        };
        
        var result = await _mediator.Send(command);

        return new ApiResult { Succeeded = result.IsSuccess };
    }
}
