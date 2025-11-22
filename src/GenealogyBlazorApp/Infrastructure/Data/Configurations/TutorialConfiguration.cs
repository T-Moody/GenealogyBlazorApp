using GenealogyBlazorApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenealogyBlazorApp.Infrastructure.Data.Configurations;

public class TutorialConfiguration : IEntityTypeConfiguration<Tutorial>
{
    public void Configure(EntityTypeBuilder<Tutorial> builder)
    {
        builder.ToTable("Tutorials");
        
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(t => t.Slug)
            .IsRequired()
            .HasMaxLength(250);
            
        builder.Property(t => t.Description)
            .HasMaxLength(1000);
            
        builder.Property(t => t.Content)
            .IsRequired();
            
        builder.Property(t => t.YouTubeUrl)
            .HasMaxLength(500);
            
        builder.Property(t => t.EstimatedDurationMinutes)
            .HasDefaultValue(5);
            
        builder.Property(t => t.ViewCount)
            .HasDefaultValue(0);
            
        builder.Property(t => t.CreatedBy)
            .HasMaxLength(100);
            
        builder.Property(t => t.UpdatedBy)
            .HasMaxLength(100);
            
        // Relationships
        builder.HasOne(t => t.Category)
            .WithMany(c => c.Tutorials)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Indexes
        builder.HasIndex(t => t.Slug)
            .IsUnique();
            
        builder.HasIndex(t => t.PublishedAt);
        
        builder.HasIndex(t => t.CategoryId);
    }
}