using GenealogyBlazorApp.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GenealogyBlazorApp.Features.Home.Commands;

public record UpdateHomeContentCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string SiteTitle { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string AboutContent { get; set; } = string.Empty;
    public string SidebarLinks { get; set; } = string.Empty;
    public IFormFile? HeroImage { get; set; }
    public IFormFile? ProfileImage { get; set; }
}