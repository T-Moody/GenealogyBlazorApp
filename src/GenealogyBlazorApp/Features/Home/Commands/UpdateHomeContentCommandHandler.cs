using GenealogyBlazorApp.Core.Shared;
using GenealogyBlazorApp.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GenealogyBlazorApp.Features.Home.Commands;

public class UpdateHomeContentCommandHandler : IRequestHandler<UpdateHomeContentCommand, Result>
{
    private readonly AppDbContext _context;
    private readonly ILogger<UpdateHomeContentCommandHandler> _logger;

    public UpdateHomeContentCommandHandler(AppDbContext context, ILogger<UpdateHomeContentCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateHomeContentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating home content for ID: {Id}", request.Id);

        var homeContent = await _context.HomeContent.FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

        if (homeContent is null)
        {
            _logger.LogWarning("Home content with ID: {Id} not found.", request.Id);
            return Result.Failure($"Home content with ID: {request.Id} not found.");
        }

        homeContent.SiteTitle = request.SiteTitle;
        homeContent.Tagline = request.Tagline;
        homeContent.AboutContent = request.AboutContent;
        homeContent.SidebarLinks = request.SidebarLinks;

        // Note: Image handling will be added in a separate step
        // For now, we are just updating text content.

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Successfully updated home content for ID: {Id}", request.Id);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating home content for ID: {Id}", request.Id);
            return Result.Failure($"An error occurred while updating home content: {ex.Message}");
        }
    }
}