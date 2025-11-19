using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenealogyBlazorApp.Domain.Entities;

// Core entity for counties/categories
public class County
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    [MaxLength(500)]
    public string? ImageUrl { get; set; }
    
    public int DisplayOrder { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Navigation properties
    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();
}

// Main resource entity for all content types
public class Resource
{
    [Key]
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
    
    // For rich content (articles, guides)
    public string? Content { get; set; }
    
    public int DisplayOrder { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    [MaxLength(500)]
    public string? ThumbnailUrl { get; set; }
    
    // For videos - store embed ID
    [MaxLength(100)]
    public string? VideoId { get; set; }
    
    // For documents - file metadata
    [MaxLength(50)]
    public string? FileSize { get; set; }
    
    [MaxLength(20)]
    public string? FileType { get; set; }
    
    // Navigation properties
    [ForeignKey(nameof(CountyId))]
    public virtual County County { get; set; } = null!;
    
    public virtual ICollection<ResourceTag> ResourceTags { get; set; } = new List<ResourceTag>();
}

// Enum for resource types
public enum ResourceType
{
    Video = 1,
    Link = 2, 
    Document = 3,
    Article = 4
}

// Tag system for better organization and search
public class Tag
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [MaxLength(20)]
    public string? Color { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    public virtual ICollection<ResourceTag> ResourceTags { get; set; } = new List<ResourceTag>();
}

// Junction table for many-to-many relationship
public class ResourceTag
{
    public int ResourceId { get; set; }
    public int TagId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    // Navigation properties
    [ForeignKey(nameof(ResourceId))]
    public virtual Resource Resource { get; set; } = null!;
    
    [ForeignKey(nameof(TagId))]
    public virtual Tag Tag { get; set; } = null!;
}

// Site-wide settings for admin management
public class SiteSettings
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Key { get; set; } = string.Empty;
    
    public string? Value { get; set; }
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    [MaxLength(100)]
    public string? UpdatedBy { get; set; }
}

// Admin user entity (extends Identity if needed, or standalone)
public class AdminUser
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string? Email { get; set; }
    
    [MaxLength(100)]
    public string? DisplayName { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; }
    public DateTime LastLoginAt { get; set; }
    
    [MaxLength(200)]
    public string? LastLoginIp { get; set; }
}