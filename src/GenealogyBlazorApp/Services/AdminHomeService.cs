using GenealogyBlazorApp.Features.Home.Commands;
using GenealogyBlazorApp.Features.Home.Queries;
using GenealogyBlazorApp.Shared.DTOs;
using GenealogyBlazorApp.Shared.Services;
using MediatR;

namespace GenealogyBlazorApp.Services;

public class AdminHomeService : IAdminHomeService
{
    private readonly IMediator _mediator;

    public AdminHomeService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<HomeContentDto?> GetHomeContentAsync()
    {
        return await _mediator.Send(new GetHomeContentQuery());
    }

    public async Task UpdateHomeContentAsync(UpdateHomeContentRequest request)
    {
        await _mediator.Send(new UpdateHomeContentCommand(request));
    }
}
