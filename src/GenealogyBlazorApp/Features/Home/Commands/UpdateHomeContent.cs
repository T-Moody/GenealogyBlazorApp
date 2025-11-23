using GenealogyBlazorApp.Infrastructure.Data;
using GenealogyBlazorApp.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GenealogyBlazorApp.Features.Home.Commands;

public record UpdateHomeContentCommand(UpdateHomeContentRequest Dto) : IRequest;

public class UpdateHomeContentHandler : IRequestHandler<UpdateHomeContentCommand, Unit>
{
    private readonly AppDbContext _context;

    public UpdateHomeContentHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateHomeContentCommand request, CancellationToken cancellationToken)
    {
        var home = await _context.Home.FirstOrDefaultAsync(cancellationToken);
        if (home is null)
        {
            // Or create a new one if that's the desired behavior
            throw new InvalidOperationException("Home content not found.");
        }

        home.SiteTitle = request.Dto.SiteTitle;
        home.Tagline = request.Dto.Tagline;
        home.AboutContent = request.Dto.AboutContent;
        home.HeroImagePath = request.Dto.HeroImagePath;
        home.ProfileImagePath = request.Dto.ProfileImagePath;
        home.SidebarLinks = request.Dto.SidebarLinks;

        // County Cards
        home.HuronTitle = request.Dto.HuronTitle;
        home.HuronImagePath = request.Dto.HuronImagePath;
        home.SanilacTitle = request.Dto.SanilacTitle;
        home.SanilacImagePath = request.Dto.SanilacImagePath;
        home.TuscolaTitle = request.Dto.TuscolaTitle;
        home.TuscolaImagePath = request.Dto.TuscolaImagePath;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
