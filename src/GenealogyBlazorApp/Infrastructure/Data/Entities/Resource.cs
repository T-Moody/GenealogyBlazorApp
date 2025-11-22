namespace GenealogyBlazorApp.Infrastructure.Data.Entities;

/// <summary>
/// Resource entity for external genealogy resources and links
/// </summary>
public class Resource : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    
    public string Url { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public ResourceType ResourceType { get; set; }
    
    public AccessLevel AccessLevel { get; set; }
    
    public string Tags { get; set; } = string.Empty;
    
    public DateTime? LastVerified { get; set; }
    
    public int ClickCount { get; set; } = 0;
    
    public bool IsActive { get; set; } = true;
    
    // Foreign key
    public int CategoryId { get; set; }
    
    // Navigation properties
    public virtual Category Category { get; set; } = null!;
    
    public virtual ICollection<ResourceCounty> ResourceCounties { get; set; } = new List<ResourceCounty>();
}

/// <summary>
/// Junction table for many-to-many relationship between Resources and Counties
/// </summary>
public class ResourceCounty
{
    public int ResourceId { get; set; }
    public int CountyId { get; set; }
    
    // Navigation properties
    public virtual Resource Resource { get; set; } = null!;
    public virtual County County { get; set; } = null!;
}

/// <summary>
/// Types of genealogy resources
/// </summary>
public enum ResourceType
{
    Website = 1,
    Archive = 2,
    Database = 3,
    Organization = 4,
    Government = 5,
    Library = 6,
    Cemetery = 7,
    Church = 8
}

/// <summary>
/// Access levels for resources
/// </summary>
public enum AccessLevel
{
    Free = 1,
    Paid = 2,
    RegistrationRequired = 3,
    MembershipRequired = 4
}