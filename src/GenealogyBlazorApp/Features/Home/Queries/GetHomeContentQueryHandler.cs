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
        try
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
                
                HuronTitle = home.HuronTitle,
                HuronImagePath = home.HuronImagePath,
                SanilacTitle = home.SanilacTitle,
                SanilacImagePath = home.SanilacImagePath,
                TuscolaTitle = home.TuscolaTitle,
                TuscolaImagePath = home.TuscolaImagePath,

                LastUpdated = home.UpdatedAt,
                LastUpdatedBy = home.UpdatedBy ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting home content");
            throw;
        }
    }
}
