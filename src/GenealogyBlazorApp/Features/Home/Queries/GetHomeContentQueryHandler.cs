using GenealogyBlazorApp.Infrastructure.Data;
using GenealogyBlazorApp.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GenealogyBlazorApp.Features.Home.Queries;

public class GetHomeContentQueryHandler : IRequestHandler<GetHomeContentQuery, HomeContentDto?>
{
    private readonly AppDbContext _context;

    public GetHomeContentQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HomeContentDto?> Handle(GetHomeContentQuery request, CancellationToken cancellationToken)
    {
        var home = await _context.Home.AsNoTracking().FirstOrDefaultAsync(cancellationToken);

        if (home is null)
        {
            return null;
        }

        return new HomeContentDto
        {
            SiteTitle = home.SiteTitle,
            Tagline = home.Tagline,
            AboutContent = home.AboutContent,
            SidebarLinks = home.SidebarLinks,
            HeroImagePath = home.HeroImagePath,
            ProfileImagePath = home.ProfileImagePath,
            LastUpdated = home.LastUpdated,
            LastUpdatedBy = home.LastUpdatedBy ?? string.Empty
        };
    }
}
