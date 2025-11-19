using GenealogyBlazorApp.Domain.Entities;
using GenealogyBlazorApp.Infrastructure.Data;
using GenealogyBlazorApp.Shared.Features.Authentication.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GenealogyBlazorApp.Features.Authentication.Services;

/// <summary>
/// Domain service for authentication operations - used by server-side handlers
/// </summary>
public interface IAuthenticationDomainService
{
    Task<LoginResponse> AuthenticateAsync(string username, string password);
    Task SignOutAsync();
    Task<bool> IsAuthenticatedAsync();
}

public class AuthenticationDomainService : IAuthenticationDomainService
{
    private readonly GenealogyDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationDomainService(GenealogyDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<LoginResponse> AuthenticateAsync(string username, string password)
    {
        var user = await GetActiveUserByUsernameAsync(username);
        
        if (user == null || !VerifyPassword(password, user.PasswordHash))
        {
            return new LoginResponse(false, "Invalid username or password");
        }

        await UpdateLastLoginAsync(user);
        await CreateUserSessionAsync(user);

        return new LoginResponse(true, Username: user.Username);
    }

    public async Task SignOutAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }

    public Task<bool> IsAuthenticatedAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        return Task.FromResult(httpContext?.User?.Identity?.IsAuthenticated == true);
    }

    private async Task<AdminUser?> GetActiveUserByUsernameAsync(string username)
    {
        return await _dbContext.AdminUsers
            .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
    }

    private static bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    private async Task UpdateLastLoginAsync(AdminUser user)
    {
        user.LastLoginAt = DateTime.UtcNow;
        user.LastLoginIp = GetClientIpAddress();
        await _dbContext.SaveChangesAsync();
    }

    private string? GetClientIpAddress()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) return null;

        var xForwardedFor = httpContext.Request.Headers["X-Forwarded-For"];
        if (!string.IsNullOrEmpty(xForwardedFor))
        {
            return xForwardedFor.ToString().Split(',')[0].Trim();
        }

        return httpContext.Connection.RemoteIpAddress?.ToString();
    }

    private async Task CreateUserSessionAsync(AdminUser user)
    {
        var claims = CreateUserClaims(user);
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            return;
        }

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            CreateAuthenticationProperties());
    }

    private static List<Claim> CreateUserClaims(AdminUser user)
    {
        return new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new("admin", "true")
        };
    }

    private static AuthenticationProperties CreateAuthenticationProperties()
    {
        return new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        };
    }
}

// Simple DbContext for authentication - can be moved to Infrastructure later
public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    public DbSet<AdminUser> AdminUsers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<SiteConfiguration> SiteConfigurations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // AdminUser configuration
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.HasIndex(e => e.Username).IsUnique();
        });

        // Category configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.DisplayOrder);
        });

        // Resource configuration  
        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.AccessUrl).IsRequired().HasMaxLength(500);
            entity.HasIndex(e => new { e.CategoryId, e.DisplayOrder });
            
            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Resources)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Site Configuration
        modelBuilder.Entity<SiteConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SiteName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.MainPageTitle).IsRequired().HasMaxLength(200);
            entity.Property(e => e.MainPageContent).HasMaxLength(2000);
            entity.Property(e => e.BannerImageUrl).HasMaxLength(500);
            entity.Property(e => e.ContactInfo).HasMaxLength(500);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed initial counties
        modelBuilder.Entity<Category>().HasData(
            new Category 
            { 
                Id = 1, 
                Name = "Huron County", 
                Description = "Genealogy resources for Huron County, Michigan", 
                DisplayOrder = 1, 
                IsVisible = true, 
                CreatedDate = DateTime.UtcNow, 
                ModifiedDate = DateTime.UtcNow 
            },
            new Category 
            { 
                Id = 2, 
                Name = "Tuscola County", 
                Description = "Genealogy resources for Tuscola County, Michigan", 
                DisplayOrder = 2, 
                IsVisible = true, 
                CreatedDate = DateTime.UtcNow, 
                ModifiedDate = DateTime.UtcNow 
            },
            new Category 
            { 
                Id = 3, 
                Name = "Sanilac County", 
                Description = "Genealogy resources for Sanilac County, Michigan", 
                DisplayOrder = 3, 
                IsVisible = true, 
                CreatedDate = DateTime.UtcNow, 
                ModifiedDate = DateTime.UtcNow 
            }
        );

        // Seed default site configuration
        modelBuilder.Entity<SiteConfiguration>().HasData(
            new SiteConfiguration
            {
                Id = 1,
                SiteName = "Michigan Genealogy Resources",
                MainPageTitle = "Welcome to Michigan Genealogy Resources",
                MainPageContent = "Discover genealogy resources for Michigan counties. Browse by county to find tutorials, documents, links, and guides for your family history research.",
                ModifiedDate = DateTime.UtcNow
            }
        );

        // Seed default admin user (password: "admin123")
        modelBuilder.Entity<AdminUser>().HasData(
            new AdminUser
            {
                Id = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                LastLoginDate = DateTime.UtcNow,
                IsActive = true
            }
        );
    }
}