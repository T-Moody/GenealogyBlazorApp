using GenealogyBlazorApp.Shared.DTOs;
using MediatR;

namespace GenealogyBlazorApp.Features.Home.Queries;

public record GetPublicHomeContentQuery : IRequest<PublicHomeContentDto?>;
