using GenealogyBlazorApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenealogyBlazorApp.Infrastructure.Data.Configurations;

public class HomeConfiguration : IEntityTypeConfiguration<Home>
{
    public void Configure(EntityTypeBuilder<Home> builder)
    {
        builder.ToTable("Home");
        
        builder.HasKey(h => h.Id);
        
        builder.Property(h => h.SiteTitle)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(h => h.Tagline)
            .HasMaxLength(200);
            
        builder.Property(h => h.AboutContent)
            .IsRequired();
            
        builder.Property(h => h.HeroImagePath)
            .HasMaxLength(500);
            
        builder.Property(h => h.ProfileImagePath)
            .HasMaxLength(500);
            
        builder.Property(h => h.SidebarLinks)
            .HasMaxLength(2000);
            
        builder.Property(h => h.CreatedBy)
            .HasMaxLength(100);
            
        builder.Property(h => h.UpdatedBy)
            .HasMaxLength(100);
    }
}