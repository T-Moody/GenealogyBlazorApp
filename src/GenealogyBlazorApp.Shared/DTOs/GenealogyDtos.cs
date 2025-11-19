using System.ComponentModel.DataAnnotations;

namespace GenealogyBlazorApp.Shared.DTOs;

// County DTOs
public class CountyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ResourceCount { get; set; }
}

public class CreateCountyRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    [MaxLength(500)]
    public string? ImageUrl { get; set; }
    
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UpdateCountyRequest
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    [MaxLength(500)]
    public string? ImageUrl { get; set; }
    
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}

// Resource DTOs
public class ResourceDto
{
    public int Id { get; set; }
    public int CountyId { get; set; }
    public string CountyName { get; set; } = string.Empty;
    public ResourceType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? Content { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? VideoId { get; set; }
    public string? FileSize { get; set; }
    public string? FileType { get; set; }
    public List<TagDto> Tags { get; set; } = new();
}

public class CreateResourceRequest
{
    public int CountyId { get; set; }
    
    [Required]
    public ResourceType Type { get; set; }
    
    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    [MaxLength(1000)]
    public string? Url { get; set; }
    
    public string? Content { get; set; }
    
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    
    [MaxLength(500)]
    public string? ThumbnailUrl { get; set; }
    
    [MaxLength(100)]
    public string? VideoId { get; set; }
    
    [MaxLength(50)]
    public string? FileSize { get; set; }
    
    [MaxLength(20)]
    public string? FileType { get; set; }
    
    public List<int> TagIds { get; set; } = new();
}

public class UpdateResourceRequest
{
    public int Id { get; set; }
    public int CountyId { get; set; }
    
    [Required]
    public ResourceType Type { get; set; }
    
    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    [MaxLength(1000)]
    public string? Url { get; set; }
    
    public string? Content { get; set; }
    
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    
    [MaxLength(500)]
    public string? ThumbnailUrl { get; set; }
    
    [MaxLength(100)]
    public string? VideoId { get; set; }
    
    [MaxLength(50)]
    public string? FileSize { get; set; }
    
    [MaxLength(20)]
    public string? FileType { get; set; }
    
    public List<int> TagIds { get; set; } = new();
}

// Tag DTOs
public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Color { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ResourceCount { get; set; }
}

public class CreateTagRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [MaxLength(20)]
    public string? Color { get; set; }
    
    public bool IsActive { get; set; } = true;
}

public class UpdateTagRequest
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [MaxLength(20)]
    public string? Color { get; set; }
    
    public bool IsActive { get; set; }
}

// Site Settings DTOs
public class SiteSettingsDto
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string? Value { get; set; }
    public string? Description { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}

public class UpdateSiteSettingsRequest
{
    public string Key { get; set; } = string.Empty;
    public string? Value { get; set; }
    public string? UpdatedBy { get; set; }
}

// Resource Types Enum (shared)
public enum ResourceType
{
    Video = 1,
    Link = 2,
    Document = 3,
    Article = 4
}

// Search and Filter DTOs
public class ResourceSearchRequest
{
    public int? CountyId { get; set; }
    public ResourceType? Type { get; set; }
    public string? SearchTerm { get; set; }
    public List<int>? TagIds { get; set; }
    public bool ActiveOnly { get; set; } = true;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string SortBy { get; set; } = "DisplayOrder";
    public bool SortDescending { get; set; } = false;
}

public class ResourceSearchResult
{
    public List<ResourceDto> Resources { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}

// Response wrapper
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Error { get; set; }
    public List<string> ValidationErrors { get; set; } = new();
}

public class ApiResponse
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public List<string> ValidationErrors { get; set; } = new();
}