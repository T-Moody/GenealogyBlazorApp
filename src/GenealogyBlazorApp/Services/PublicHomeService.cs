using GenealogyBlazorApp.Features.Home.Queries;
using GenealogyBlazorApp.Shared.DTOs;
using GenealogyBlazorApp.Shared.Services;
using MediatR;

namespace GenealogyBlazorApp.Services;

public class PublicHomeService : IPublicHomeService
{
    private readonly IMediator _mediator;

    public PublicHomeService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<PublicHomeContentDto?> GetPublicHomeContentAsync()
    {
        return await _mediator.Send(new GetPublicHomeContentQuery());
    }
}
