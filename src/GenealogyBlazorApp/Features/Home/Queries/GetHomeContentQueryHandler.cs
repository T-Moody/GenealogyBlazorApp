using GenealogyBlazorApp.Infrastructure.Data;
using GenealogyBlazorApp.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GenealogyBlazorApp.Features.Home.Queries;

public class GetHomeContentQueryHandler : IRequestHandler<GetHomeContentQuery, HomeContentDto?>
{
    private readonly AppDbContext _context;
    private readonly ILogger<GetHomeContentQueryHandler> _logger;

    public GetHomeContentQueryHandler(AppDbContext context, ILogger<GetHomeContentQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HomeContentDto?> Handle(GetHomeContentQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching active home content");

        var homeContent = await _context.HomeContent
            .AsNoTracking()
            .Where(h => h.IsActive)
            .OrderByDescending(h => h.UpdatedAt)
            .Select(h => new HomeContentDto
            {
                Id = h.Id,
                SiteTitle = h.SiteTitle,
                Tagline = h.Tagline,
                AboutContent = h.AboutContent,
                SidebarLinks = h.SidebarLinks,
                HeroImagePath = h.HeroImagePath,
                ProfileImagePath = h.ProfileImagePath,
                UpdatedAt = h.UpdatedAt,
                UpdatedBy = h.UpdatedBy
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (homeContent is null)
        {
            _logger.LogWarning("No active home content found.");
        }

        return homeContent;
    }
}