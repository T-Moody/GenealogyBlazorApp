using GenealogyBlazorApp.Infrastructure.Data;
using GenealogyBlazorApp.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GenealogyBlazorApp.Features.Home.Queries;

public class GetPublicHomeContentQueryHandler : IRequestHandler<GetPublicHomeContentQuery, PublicHomeContentDto?>
{
    private readonly AppDbContext _context;
    private readonly ILogger<GetPublicHomeContentQueryHandler> _logger;

    public GetPublicHomeContentQueryHandler(AppDbContext context, ILogger<GetPublicHomeContentQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PublicHomeContentDto?> Handle(GetPublicHomeContentQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching public home content");

        var homeContent = await _context.Home
            .AsNoTracking()
            .OrderByDescending(h => h.UpdatedAt)
            .Select(h => new PublicHomeContentDto
            {
                SiteTitle = h.SiteTitle,
                Tagline = h.Tagline,
                AboutContent = h.AboutContent,
                SidebarLinks = h.SidebarLinks,
                HeroImagePath = h.HeroImagePath,
                ProfileImagePath = h.ProfileImagePath
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (homeContent is null)
        {
            _logger.LogWarning("No active public home content found.");
        }
        else
        {
            if (string.IsNullOrEmpty(homeContent.HeroImagePath))
            {
                homeContent.HeroImagePath = "/images/hero-image-thumb.png";
            }
        }

        return homeContent;
    }
}
