using GenealogyBlazorApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenealogyBlazorApp.Infrastructure.Data.Configurations;

public class CountyConfiguration : IEntityTypeConfiguration<County>
{
    public void Configure(EntityTypeBuilder<County> builder)
    {
        builder.ToTable("Counties");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(c => c.DisplayName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(c => c.Description)
            .HasMaxLength(1000);
            
        builder.Property(c => c.CountySeat)
            .HasMaxLength(100);
            
        builder.Property(c => c.HistoricalNotes)
            .HasMaxLength(2000);
            
        builder.Property(c => c.HeaderImagePath)
            .HasMaxLength(500);
            
        builder.Property(c => c.CreatedBy)
            .HasMaxLength(100);
            
        builder.Property(c => c.UpdatedBy)
            .HasMaxLength(100);
            
        // Indexes
        builder.HasIndex(c => c.Name)
            .IsUnique();
            
        builder.HasIndex(c => c.DisplayOrder);
            
        // TODO: Create separate seeding mechanism for Michigan counties data
    }
}