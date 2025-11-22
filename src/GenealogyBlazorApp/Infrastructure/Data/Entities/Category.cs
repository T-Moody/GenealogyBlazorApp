namespace GenealogyBlazorApp.Infrastructure.Data.Entities;

/// <summary>
/// Category entity for organizing tutorials and resources
/// </summary>
public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string ColorTheme { get; set; } = string.Empty;
    
    public int DisplayOrder { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public virtual ICollection<Tutorial> Tutorials { get; set; } = new List<Tutorial>();
    
    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();
}