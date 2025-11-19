using GenealogyBlazorApp.Features.Authentication.Authorization;
using GenealogyBlazorApp.Infrastructure.Data;
using GenealogyBlazorApp.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GenealogyBlazorApp.Features.Counties.Endpoints;

public static class CountiesEndpoints
{
    public static void MapCountiesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/counties")
            .WithTags("Counties");

        // Public endpoints (no auth required)
        group.MapGet("/", GetCountiesAsync);
        group.MapGet("/{id:int}", GetCountyAsync);

        // Admin only endpoints
        group.MapPost("/", CreateCountyAsync)
            .RequireAuthorization(AuthorizationPolicies.AdminOnly);
            
        group.MapPut("/{id:int}", UpdateCountyAsync)
            .RequireAuthorization(AuthorizationPolicies.AdminOnly);
            
        group.MapDelete("/{id:int}", DeleteCountyAsync)
            .RequireAuthorization(AuthorizationPolicies.AdminOnly);
            
        group.MapPost("/reorder", ReorderCountiesAsync)
            .RequireAuthorization(AuthorizationPolicies.AdminOnly);
    }

    private static async Task<IResult> GetCountiesAsync(
        GenealogyDbContext context, 
        bool activeOnly = true)
    {
        try
        {
            var query = context.Counties.AsQueryable();
            
            if (activeOnly)
            {
                query = query.Where(c => c.IsActive);
            }

            var counties = await query
                .Include(c => c.Resources.Where(r => r.IsActive))
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .Select(c => new CountyDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    DisplayOrder = c.DisplayOrder,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    ResourceCount = c.Resources.Count
                })
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<CountyDto>>
            {
                Success = true,
                Data = counties
            });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error retrieving counties: {ex.Message}");
        }
    }

    private static async Task<IResult> GetCountyAsync(
        int id,
        GenealogyDbContext context)
    {
        try
        {
            var county = await context.Counties
                .Include(c => c.Resources.Where(r => r.IsActive))
                .Where(c => c.Id == id)
                .Select(c => new CountyDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    DisplayOrder = c.DisplayOrder,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    ResourceCount = c.Resources.Count
                })
                .FirstOrDefaultAsync();

            if (county == null)
            {
                return Results.NotFound(new ApiResponse<CountyDto>
                {
                    Success = false,
                    Error = "County not found"
                });
            }

            return Results.Ok(new ApiResponse<CountyDto>
            {
                Success = true,
                Data = county
            });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error retrieving county: {ex.Message}");
        }
    }

    private static async Task<IResult> CreateCountyAsync(
        CreateCountyRequest request,
        GenealogyDbContext context)
    {
        try
        {
            // Check for duplicate name
            var exists = await context.Counties
                .AnyAsync(c => c.Name == request.Name);

            if (exists)
            {
                return Results.BadRequest(new ApiResponse<CountyDto>
                {
                    Success = false,
                    Error = "A county with this name already exists"
                });
            }

            var county = new Domain.Entities.County
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                DisplayOrder = request.DisplayOrder,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Counties.Add(county);
            await context.SaveChangesAsync();

            var dto = new CountyDto
            {
                Id = county.Id,
                Name = county.Name,
                Description = county.Description,
                ImageUrl = county.ImageUrl,
                DisplayOrder = county.DisplayOrder,
                IsActive = county.IsActive,
                CreatedAt = county.CreatedAt,
                UpdatedAt = county.UpdatedAt,
                ResourceCount = 0
            };

            return Results.Created($"/api/counties/{county.Id}", new ApiResponse<CountyDto>
            {
                Success = true,
                Data = dto
            });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error creating county: {ex.Message}");
        }
    }

    private static async Task<IResult> UpdateCountyAsync(
        int id,
        UpdateCountyRequest request,
        GenealogyDbContext context)
    {
        try
        {
            if (id != request.Id)
            {
                return Results.BadRequest(new ApiResponse<CountyDto>
                {
                    Success = false,
                    Error = "ID mismatch"
                });
            }

            var county = await context.Counties.FindAsync(id);
            if (county == null)
            {
                return Results.NotFound(new ApiResponse<CountyDto>
                {
                    Success = false,
                    Error = "County not found"
                });
            }

            // Check for duplicate name (excluding current county)
            var exists = await context.Counties
                .AnyAsync(c => c.Name == request.Name && c.Id != id);

            if (exists)
            {
                return Results.BadRequest(new ApiResponse<CountyDto>
                {
                    Success = false,
                    Error = "A county with this name already exists"
                });
            }

            county.Name = request.Name;
            county.Description = request.Description;
            county.ImageUrl = request.ImageUrl;
            county.DisplayOrder = request.DisplayOrder;
            county.IsActive = request.IsActive;

            await context.SaveChangesAsync();

            var resourceCount = await context.Resources
                .CountAsync(r => r.CountyId == id && r.IsActive);

            var dto = new CountyDto
            {
                Id = county.Id,
                Name = county.Name,
                Description = county.Description,
                ImageUrl = county.ImageUrl,
                DisplayOrder = county.DisplayOrder,
                IsActive = county.IsActive,
                CreatedAt = county.CreatedAt,
                UpdatedAt = county.UpdatedAt,
                ResourceCount = resourceCount
            };

            return Results.Ok(new ApiResponse<CountyDto>
            {
                Success = true,
                Data = dto
            });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error updating county: {ex.Message}");
        }
    }

    private static async Task<IResult> DeleteCountyAsync(
        int id,
        GenealogyDbContext context)
    {
        try
        {
            var county = await context.Counties
                .Include(c => c.Resources)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (county == null)
            {
                return Results.NotFound(new ApiResponse
                {
                    Success = false,
                    Error = "County not found"
                });
            }

            // Check if county has resources
            if (county.Resources.Any())
            {
                return Results.BadRequest(new ApiResponse
                {
                    Success = false,
                    Error = "Cannot delete county with existing resources. Please remove all resources first."
                });
            }

            context.Counties.Remove(county);
            await context.SaveChangesAsync();

            return Results.Ok(new ApiResponse
            {
                Success = true
            });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error deleting county: {ex.Message}");
        }
    }

    private static async Task<IResult> ReorderCountiesAsync(
        List<int> countyIds,
        GenealogyDbContext context)
    {
        try
        {
            var counties = await context.Counties
                .Where(c => countyIds.Contains(c.Id))
                .ToListAsync();

            for (int i = 0; i < countyIds.Count; i++)
            {
                var county = counties.FirstOrDefault(c => c.Id == countyIds[i]);
                if (county != null)
                {
                    county.DisplayOrder = i + 1;
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
            return Results.Problem($"Error reordering counties: {ex.Message}");
        }
    }
}