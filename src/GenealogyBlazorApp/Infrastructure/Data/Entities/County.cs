namespace GenealogyBlazorApp.Infrastructure.Data.Entities;

/// <summary>
/// County entity representing Michigan Thumb region counties
/// </summary>
public class County : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    
    public string DisplayName { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public DateTime? FoundedDate { get; set; }
    
    public string CountySeat { get; set; } = string.Empty;
    
    public string HistoricalNotes { get; set; } = string.Empty;
    
    public string? HeaderImagePath { get; set; }
    
    public int DisplayOrder { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public virtual ICollection<ResourceCounty> ResourceCounties { get; set; } = new List<ResourceCounty>();
}