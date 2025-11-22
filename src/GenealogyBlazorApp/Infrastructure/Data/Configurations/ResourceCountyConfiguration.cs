using GenealogyBlazorApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenealogyBlazorApp.Infrastructure.Data.Configurations;

public class ResourceCountyConfiguration : IEntityTypeConfiguration<ResourceCounty>
{
    public void Configure(EntityTypeBuilder<ResourceCounty> builder)
    {
        builder.ToTable("ResourceCounties");
        
        // Composite primary key
        builder.HasKey(rc => new { rc.ResourceId, rc.CountyId });
        
        // Relationships
        builder.HasOne(rc => rc.Resource)
            .WithMany(r => r.ResourceCounties) 
            .HasForeignKey(rc => rc.ResourceId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(rc => rc.County)
            .WithMany(c => c.ResourceCounties)
            .HasForeignKey(rc => rc.CountyId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Indexes
        builder.HasIndex(rc => rc.ResourceId);
        builder.HasIndex(rc => rc.CountyId);
    }
}