using GenealogyBlazorApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenealogyBlazorApp.Infrastructure.Data;

public class GenealogyDbContext : DbContext
{
    public GenealogyDbContext(DbContextOptions<GenealogyDbContext> options) : base(options)
    {
    }

    // DbSets for genealogy domain
    public DbSet<County> Counties { get; set; } = null!;
    public DbSet<Resource> Resources { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<ResourceTag> ResourceTags { get; set; } = null!;
    public DbSet<SiteSettings> SiteSettings { get; set; } = null!;
    public DbSet<AdminUser> AdminUsers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // County configuration
        modelBuilder.Entity<County>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.DisplayOrder);
            entity.HasIndex(e => e.IsActive);
        });

        // Resource configuration
        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(300);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Url).HasMaxLength(1000);
            entity.Property(e => e.ThumbnailUrl).HasMaxLength(500);
            entity.Property(e => e.VideoId).HasMaxLength(100);
            entity.Property(e => e.FileSize).HasMaxLength(50);
            entity.Property(e => e.FileType).HasMaxLength(20);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            
            entity.HasIndex(e => e.CountyId);
            entity.HasIndex(e => e.Type);
            entity.HasIndex(e => e.IsActive);
            entity.HasIndex(e => e.DisplayOrder);
            entity.HasIndex(e => e.Title);
            
            // Foreign key relationship
            entity.HasOne(e => e.County)
                  .WithMany(c => c.Resources)
                  .HasForeignKey(e => e.CountyId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Tag configuration
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Color).HasMaxLength(20);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.IsActive);
        });

        // ResourceTag junction table configuration
        modelBuilder.Entity<ResourceTag>(entity =>
        {
            entity.HasKey(e => new { e.ResourceId, e.TagId });
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            
            entity.HasOne(e => e.Resource)
                  .WithMany(r => r.ResourceTags)
                  .HasForeignKey(e => e.ResourceId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Tag)
                  .WithMany(t => t.ResourceTags)
                  .HasForeignKey(e => e.TagId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // SiteSettings configuration
        modelBuilder.Entity<SiteSettings>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Key).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            
            entity.HasIndex(e => e.Key).IsUnique();
        });

        // AdminUser configuration
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.LastLoginIp).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.IsActive);
        });

        // Seed development data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed default admin user (password: "admin123")
        modelBuilder.Entity<AdminUser>().HasData(
            new AdminUser
            {
                Id = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Email = "admin@genealogy.local",
                DisplayName = "Administrator",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            }
        );

        // Seed initial counties
        modelBuilder.Entity<County>().HasData(
            new County
            {
                Id = 1,
                Name = "Huron County",
                Description = "Genealogy resources for Huron County, Michigan. Located in the Thumb region, established in 1859.",
                DisplayOrder = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new County
            {
                Id = 2,
                Name = "Tuscola County", 
                Description = "Genealogy resources for Tuscola County, Michigan. Home to Caro and many historical records.",
                DisplayOrder = 2,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new County
            {
                Id = 3,
                Name = "Sanilac County",
                Description = "Genealogy resources for Sanilac County, Michigan. Rich agricultural history and family records.",
                DisplayOrder = 3,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Seed sample tags
        modelBuilder.Entity<Tag>().HasData(
            new Tag
            {
                Id = 1,
                Name = "Cemetery Records",
                Description = "Resources related to cemetery and burial records",
                Color = "#28a745",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Tag
            {
                Id = 2,
                Name = "Marriage Records",
                Description = "Marriage certificates and related documents",
                Color = "#dc3545",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Tag
            {
                Id = 3,
                Name = "Birth Records",
                Description = "Birth certificates and vital records",
                Color = "#007bff",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Tag
            {
                Id = 4,
                Name = "Census Data",
                Description = "US Census records and population data",
                Color = "#ffc107",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        );

        // Seed site settings
        modelBuilder.Entity<SiteSettings>().HasData(
            new SiteSettings
            {
                Id = 1,
                Key = "SiteName",
                Value = "Michigan Genealogy Resources",
                Description = "The name of the website",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "System"
            },
            new SiteSettings
            {
                Id = 2,
                Key = "MainPageTitle",
                Value = "Welcome to Michigan Genealogy Resources",
                Description = "Title displayed on the main page",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "System"
            },
            new SiteSettings
            {
                Id = 3,
                Key = "MainPageDescription",
                Value = "Discover genealogy resources for Michigan counties. Browse by county to find tutorials, documents, links, and guides for your family history research.",
                Description = "Description text for the main page",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "System"
            }
        );

        // Seed sample resources
        modelBuilder.Entity<Resource>().HasData(
            new Resource
            {
                Id = 1,
                CountyId = 1,
                Type = ResourceType.Link,
                Title = "Huron County Genealogical Society",
                Description = "Official website of the Huron County Genealogical Society with research resources and publications.",
                Url = "https://www.huroncountygenealogy.org",
                DisplayOrder = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Resource
            {
                Id = 2,
                CountyId = 1,
                Type = ResourceType.Video,
                Title = "Researching in Huron County - Tutorial",
                Description = "A comprehensive video guide on how to research family history in Huron County, Michigan.",
                Url = "https://www.youtube.com/watch?v=example",
                VideoId = "example",
                DisplayOrder = 2,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }

    // Override SaveChanges to automatically update timestamps
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity is County county)
            {
                county.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.Entity is Resource resource)
            {
                resource.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.Entity is SiteSettings settings)
            {
                settings.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}