using GenealogyBlazorApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GenealogyBlazorApp.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                // Apply any pending migrations
                await dbContext.Database.MigrateAsync();

                // Seed Home Content
                if (!await dbContext.Home.AnyAsync())
                {
                    var homeContent = new Home
                    {
                        SiteTitle = "Thumb of Michigan Genealogy",
                        Tagline = "Discovering Your Roots in Michigan's Thumb",
                        AboutContent = "Welcome to your genealogy journey! This site is dedicated to helping you explore the rich history of families in the Thumb of Michigan. Our resources and tutorials are designed to guide you, whether you're a beginner or an experienced researcher.",
                        SidebarLinks = @"<ul class=""county-list"">
                            <li><a href=""#"">Huron County Records</a></li>
                            <li><a href=""#"">Tuscola County Archives</a></li>
                            <li><a href=""#"">Sanilac County History</a></li>
                        </ul>", // Empty JSON array for links
                        HeroImagePath = "/images/hero-image-thumb.png",
                        ProfileImagePath = "/images/profile-image.jpg"
                    };
                    await dbContext.Home.AddAsync(homeContent);
                    await dbContext.SaveChangesAsync();
                }

                // Seed Admin User (if not exists)
                var adminEmail = "admin@example.com";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(adminUser, "Admin@123");

                    if (result.Succeeded)
                    {
                        // In a real app, you'd create an 'Admin' role and assign it.
                        // For simplicity, we're just creating the user for now.
                    }
                }
            }
        }
    }
}
