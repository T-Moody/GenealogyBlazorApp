using GenealogyBlazorApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenealogyBlazorApp.Infrastructure.Data.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable("Resources");
        
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(r => r.Description)
            .HasMaxLength(1000);
            
        builder.Property(r => r.Url)
            .HasMaxLength(500);
            
        builder.Property(r => r.Tags)
            .HasMaxLength(500);
            
        builder.Property(r => r.CreatedBy)
            .HasMaxLength(100);
            
        builder.Property(r => r.UpdatedBy)
            .HasMaxLength(100);
            
        // Relationships
        builder.HasOne(r => r.Category)
            .WithMany(c => c.Resources)
            .HasForeignKey(r => r.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Indexes
        builder.HasIndex(r => r.CategoryId);
        builder.HasIndex(r => r.ResourceType);
        builder.HasIndex(r => r.AccessLevel);
    }
}