using GenealogyBlazorApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenealogyBlazorApp.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(c => c.Description)
            .HasMaxLength(500);
            
        builder.Property(c => c.ColorTheme)
            .HasMaxLength(50);
            
        builder.Property(c => c.CreatedBy)
            .HasMaxLength(100);
            
        builder.Property(c => c.UpdatedBy)
            .HasMaxLength(100);
            
        // Indexes
        builder.HasIndex(c => c.Name)
            .IsUnique();
            
        builder.HasIndex(c => c.DisplayOrder);
    }
}