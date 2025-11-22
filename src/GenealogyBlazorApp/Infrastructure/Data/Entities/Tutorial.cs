namespace GenealogyBlazorApp.Infrastructure.Data.Entities;

/// <summary>
/// Tutorial entity for genealogy learning content
/// </summary>
public class Tutorial : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    public string Slug { get; set; } = string.Empty;
    
    public string? YouTubeUrl { get; set; }
    
    public bool IsPublished { get; set; } = false;
    
    public int EstimatedDurationMinutes { get; set; }
    
    public int ViewCount { get; set; } = 0;
    
    public DateTime? PublishedAt { get; set; }
    
    // Foreign key
    public int CategoryId { get; set; }
    
    // Navigation properties
    public virtual Category Category { get; set; } = null!;
}