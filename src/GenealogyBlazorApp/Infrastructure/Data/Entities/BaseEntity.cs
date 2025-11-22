namespace GenealogyBlazorApp.Infrastructure.Data.Entities;

/// <summary>
/// Base entity with common auditing properties for all domain entities
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public string CreatedBy { get; set; } = string.Empty;
    
    public string UpdatedBy { get; set; } = string.Empty;
}