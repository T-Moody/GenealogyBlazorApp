using GenealogyBlazorApp.Features.Authentication.Authorization;
using GenealogyBlazorApp.Infrastructure.Data;
using GenealogyBlazorApp.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GenealogyBlazorApp.Features.Resources.Endpoints;

public static class ResourcesEndpoints
{
    public static void MapResourcesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/resources")
            .WithTags("Resources");

        // Public endpoints (no auth required)
        group.MapGet("/county/{countyId:int}", GetResourcesByCountyAsync);
        group.MapGet("/{id:int}", GetResourceAsync);
        group.MapGet("/search", SearchResourcesAsync);

        // Admin only endpoints
        group.MapPost("/", CreateResourceAsync)
            .RequireAuthorization(AuthorizationPolicies.AdminOnly);
            
        group.MapPut("/{id:int}", UpdateResourceAsync)
            .RequireAuthorization(AuthorizationPolicies.AdminOnly);
            
        group.MapDelete("/{id:int}", DeleteResourceAsync)
            .RequireAuthorization(AuthorizationPolicies.AdminOnly);
            
        group.MapPost("/reorder", ReorderResourcesAsync)
            .RequireAuthorization(AuthorizationPolicies.AdminOnly);
    }

    private static async Task<IResult> GetResourcesByCountyAsync(
        int countyId,
        GenealogyDbContext context,
        ResourceType? type = null,
        bool activeOnly = true)
    {
        try
        {
            var query = context.Resources
                .Include(r => r.ResourceTags)
                    .ThenInclude(rt => rt.Tag)
                .Where(r => r.CountyId == countyId);
            
            if (activeOnly)
            {
                query = query.Where(r => r.IsActive);
            }

            if (type.HasValue)
            {
                query = query.Where(r => r.Type == type.Value);
            }

            var resources = await query
                .OrderBy(r => r.DisplayOrder)
                .ThenBy(r => r.Title)
                .Select(r => new ResourceDto
                {
                    Id = r.Id,
                    CountyId = r.CountyId,
                    Type = r.Type,
                    Title = r.Title,
                    Description = r.Description,
                    Url = r.Url,
                    Content = r.Content,
                    ThumbnailUrl = r.ThumbnailUrl,
                    VideoId = r.VideoId,
                    FileSize = r.FileSize,
                    FileType = r.FileType,
                    DisplayOrder = r.DisplayOrder,
                    IsActive = r.IsActive,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    Tags = r.ResourceTags.Where(rt => rt.Tag.IsActive)
                        .Select(rt => new TagDto
                        {
                            Id = rt.Tag.Id,
                            Name = rt.Tag.Name,
                            Description = rt.Tag.Description,
                            Color = rt.Tag.Color,
                            IsActive = rt.Tag.IsActive,
                            CreatedAt = rt.Tag.CreatedAt
                        }).ToList()
                })
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<ResourceDto>>
            {
                Success = true,
                Data = resources
            });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error retrieving resources: {ex.Message}");
        }
    }

    private static async Task<IResult> GetResourceAsync(
        int id,
        GenealogyDbContext context)
    {
        try
        {
            var resource = await context.Resources
                .Include(r => r.County)
                .Include(r => r.ResourceTags)
                    .ThenInclude(rt => rt.Tag)
                .Where(r => r.Id == id)
                .Select(r => new ResourceDto
                {
                    Id = r.Id,
                    CountyId = r.CountyId,
                    CountyName = r.County.Name,
                    Type = r.Type,
                    Title = r.Title,
                    Description = r.Description,
                    Url = r.Url,
                    Content = r.Content,
                    ThumbnailUrl = r.ThumbnailUrl,
                    VideoId = r.VideoId,
                    FileSize = r.FileSize,
                    FileType = r.FileType,
                    DisplayOrder = r.DisplayOrder,
                    IsActive = r.IsActive,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    Tags = r.ResourceTags.Where(rt => rt.Tag.IsActive)
                        .Select(rt => new TagDto
                        {
                            Id = rt.Tag.Id,
                            Name = rt.Tag.Name,
                            Description = rt.Tag.Description,
                            Color = rt.Tag.Color,
                            IsActive = rt.Tag.IsActive,
                            CreatedAt = rt.Tag.CreatedAt
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (resource == null)
            {
                return Results.NotFound(new ApiResponse<ResourceDto>
                {
                    Success = false,
                    Error = "Resource not found"
                });
            }

            return Results.Ok(new ApiResponse<ResourceDto>
            {
                Success = true,
                Data = resource
            });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error retrieving resource: {ex.Message}");
        }
    }

    private static async Task<IResult> SearchResourcesAsync(
        GenealogyDbContext context,
        string? query = null,
        int? countyId = null,
        ResourceType? type = null,
        int page = 1,
        int pageSize = 20)
    {
        try
        {
            var resourceQuery = context.Resources
                .Include(r => r.County)
                .Include(r => r.ResourceTags)
                    .ThenInclude(rt => rt.Tag)
                .Where(r => r.IsActive);

            if (!string.IsNullOrWhiteSpace(query))
            {
                var searchTerm = query.Trim().ToLower();
                resourceQuery = resourceQuery.Where(r => 
                    r.Title.ToLower().Contains(searchTerm) ||
                    (r.Description != null && r.Description.ToLower().Contains(searchTerm)) ||
                    (r.Content != null && r.Content.ToLower().Contains(searchTerm)));
            }

            if (countyId.HasValue)
            {
                resourceQuery = resourceQuery.Where(r => r.CountyId == countyId.Value);
            }

            if (type.HasValue)
            {
                resourceQuery = resourceQuery.Where(r => r.Type == type.Value);
            }

            var totalCount = await resourceQuery.CountAsync();
            
            var resources = await resourceQuery
                .OrderBy(r => r.County.DisplayOrder)
                .ThenBy(r => r.DisplayOrder)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new ResourceDto
                {
                    Id = r.Id,
                    CountyId = r.CountyId,
                    CountyName = r.County.Name,
                    Type = r.Type,
                    Title = r.Title,
                    Description = r.Description,
                    Url = r.Url,
                    ThumbnailUrl = r.ThumbnailUrl,
                    VideoId = r.VideoId,
                    FileSize = r.FileSize,
                    FileType = r.FileType,
                    DisplayOrder = r.DisplayOrder,
                    IsActive = r.IsActive,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    Tags = r.ResourceTags.Where(rt => rt.Tag.IsActive)
                        .Select(rt => new TagDto
                        {
                            Id = rt.Tag.Id,
                            Name = rt.Tag.Name,
                            Color = rt.Tag.Color
                        }).ToList()
                })
                .ToListAsync();

            return Results.Ok(new ApiResponse<PagedResult<ResourceDto>>
            {
                Success = true,
                Data = new PagedResult<ResourceDto>
                {
                    Items = resources,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                }
            });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error searching resources: {ex.Message}");
        }
    }

    private static async Task<IResult> CreateResourceAsync(
        CreateResourceRequest request,
        GenealogyDbContext context)
    {
        try
        {
            // Verify county exists
            var county = await context.Counties.FindAsync(request.CountyId);
            if (county == null)
            {
                return Results.BadRequest(new ApiResponse<ResourceDto>
                {
                    Success = false,
                    Error = "County not found"
                });
            }

            var resource = new Domain.Entities.Resource
            {
                CountyId = request.CountyId,
                Type = request.Type,
                Title = request.Title,
                Description = request.Description,
                Url = request.Url,
                Content = request.Content,
                ThumbnailUrl = request.ThumbnailUrl,
                VideoId = request.VideoId,
                FileSize = request.FileSize,
                FileType = request.FileType,
                DisplayOrder = request.DisplayOrder,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Resources.Add(resource);
            await context.SaveChangesAsync();

            // Add tags if provided
            if (request.TagIds?.Any() == true)
            {
                var validTagIds = await context.Tags
                    .Where(t => request.TagIds.Contains(t.Id) && t.IsActive)
                    .Select(t => t.Id)
                    .ToListAsync();

                var resourceTags = validTagIds.Select(tagId => new Domain.Entities.ResourceTag
                {
                    ResourceId = resource.Id,
                    TagId = tagId,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

                context.ResourceTags.AddRange(resourceTags);
                await context.SaveChangesAsync();
            }

            // Return the created resource with tags
            var createdResource = await context.Resources
                .Include(r => r.ResourceTags)
                    .ThenInclude(rt => rt.Tag)
                .Where(r => r.Id == resource.Id)
                .Select(r => new ResourceDto
                {
                    Id = r.Id,
                    CountyId = r.CountyId,
                    CountyName = county.Name,
                    Type = r.Type,
                    Title = r.Title,
                    Description = r.Description,
                    Url = r.Url,
                    Content = r.Content,
                    ThumbnailUrl = r.ThumbnailUrl,
                    VideoId = r.VideoId,
                    FileSize = r.FileSize,
                    FileType = r.FileType,
                    DisplayOrder = r.DisplayOrder,
                    IsActive = r.IsActive,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    Tags = r.ResourceTags.Where(rt => rt.Tag.IsActive)
                        .Select(rt => new TagDto
                        {
                            Id = rt.Tag.Id,
                            Name = rt.Tag.Name,
                            Description = rt.Tag.Description,
                            Color = rt.Tag.Color,
                            IsActive = rt.Tag.IsActive,
                            CreatedAt = rt.Tag.CreatedAt
                        }).ToList()
                })
                .FirstAsync();

            return Results.Created($"/api/resources/{resource.Id}", new ApiResponse<ResourceDto>
            {
                Success = true,
                Data = createdResource
            });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error creating resource: {ex.Message}");
        }
    }

    private static async Task<IResult> UpdateResourceAsync(
        int id,
        UpdateResourceRequest request,
        GenealogyDbContext context)
    {
        try
        {
            if (id != request.Id)
            {
                return Results.BadRequest(new ApiResponse<ResourceDto>
                {
                    Success = false,
                    Error = "ID mismatch"
                });
            }

            var resource = await context.Resources
                .Include(r => r.ResourceTags)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resource == null)
            {
                return Results.NotFound(new ApiResponse<ResourceDto>
                {
                    Success = false,
                    Error = "Resource not found"
                });
            }

            // Verify county exists if changed
            if (resource.CountyId != request.CountyId)
            {
                var county = await context.Counties.FindAsync(request.CountyId);
                if (county == null)
                {
                    return Results.BadRequest(new ApiResponse<ResourceDto>
                    {
                        Success = false,
                        Error = "County not found"
                    });
                }
            }

            // Update resource properties
            resource.CountyId = request.CountyId;
            resource.Type = request.Type;
            resource.Title = request.Title;
            resource.Description = request.Description;
            resource.Url = request.Url;
            resource.Content = request.Content;
            resource.ThumbnailUrl = request.ThumbnailUrl;
            resource.VideoId = request.VideoId;
            resource.FileSize = request.FileSize;
            resource.FileType = request.FileType;
            resource.DisplayOrder = request.DisplayOrder;
            resource.IsActive = request.IsActive;

            // Update tags
            if (request.TagIds != null)
            {
                // Remove existing tags
                context.ResourceTags.RemoveRange(resource.ResourceTags);

                // Add new tags
                if (request.TagIds.Any())
                {
                    var validTagIds = await context.Tags
                        .Where(t => request.TagIds.Contains(t.Id) && t.IsActive)
                        .Select(t => t.Id)
                        .ToListAsync();

                    var resourceTags = validTagIds.Select(tagId => new Domain.Entities.ResourceTag
                    {
                        ResourceId = resource.Id,
                        TagId = tagId,
                        CreatedAt = DateTime.UtcNow
                    }).ToList();

                    context.ResourceTags.AddRange(resourceTags);
                }
            }

            await context.SaveChangesAsync();

            // Return updated resource with county name and tags
            var updatedResource = await context.Resources
                .Include(r => r.County)
                .Include(r => r.ResourceTags)
                    .ThenInclude(rt => rt.Tag)
                .Where(r => r.Id == id)
                .Select(r => new ResourceDto
                {
                    Id = r.Id,
                    CountyId = r.CountyId,
                    CountyName = r.County.Name,
                    Type = r.Type,
                    Title = r.Title,
                    Description = r.Description,
                    Url = r.Url,
                    Content = r.Content,
                    ThumbnailUrl = r.ThumbnailUrl,
                    VideoId = r.VideoId,
                    FileSize = r.FileSize,
                    FileType = r.FileType,
                    DisplayOrder = r.DisplayOrder,
                    IsActive = r.IsActive,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    Tags = r.ResourceTags.Where(rt => rt.Tag.IsActive)
                        .Select(rt => new TagDto
                        {
                            Id = rt.Tag.Id,
                            Name = rt.Tag.Name,
                            Description = rt.Tag.Description,
                            Color = rt.Tag.Color,
                            IsActive = rt.Tag.IsActive,
                            CreatedAt = rt.Tag.CreatedAt
                        }).ToList()
                })
                .FirstAsync();

            return Results.Ok(new ApiResponse<ResourceDto>
            {
                Success = true,
                Data = updatedResource
            });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error updating resource: {ex.Message}");
        }
    }

    private static async Task<IResult> DeleteResourceAsync(
        int id,
        GenealogyDbContext context)
    {
        try
        {
            var resource = await context.Resources
                .Include(r => r.ResourceTags)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resource == null)
            {
                return Results.NotFound(new ApiResponse
                {
                    Success = false,
                    Error = "Resource not found"
                });
            }

            // Remove related ResourceTags first
            context.ResourceTags.RemoveRange(resource.ResourceTags);
            
            // Remove the resource
            context.Resources.Remove(resource);
            await context.SaveChangesAsync();

            return Results.Ok(new ApiResponse
            {
                Success = true
            });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error deleting resource: {ex.Message}");
        }
    }

    private static async Task<IResult> ReorderResourcesAsync(
        List<int> resourceIds,
        GenealogyDbContext context)
    {
        try
        {
            var resources = await context.Resources
                .Where(r => resourceIds.Contains(r.Id))
                .ToListAsync();

            for (int i = 0; i < resourceIds.Count; i++)
            {
                var resource = resources.FirstOrDefault(r => r.Id == resourceIds[i]);
                if (resource != null)
                {
                    resource.DisplayOrder = i + 1;
                }
            }

            await context.SaveChangesAsync();

            return Results.Ok(new ApiResponse
            {
                Success = true
            });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error reordering resources: {ex.Message}");
        }
    }
}