using GenealogyBlazorApp.Client.Pages;
using GenealogyBlazorApp.Components;
using GenealogyBlazorApp.Infrastructure.Data;
using GenealogyBlazorApp.Infrastructure.Data.Entities;
using GenealogyBlazorApp.Features.Home.Endpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Database configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Data Source=genealogy.db";

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseSqlite(connectionString);
    }
    else
    {
        // Use SQL Server in production
        var sqlServerConnection = builder.Configuration.GetConnectionString("SqlServerConnection");
        options.UseSqlServer(sqlServerConnection ?? throw new InvalidOperationException("SQL Server connection string not found"));
    }
});

// Identity configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings for admin users
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    
    // User settings
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false; // Keep simple for admin-only system
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// MediatR for CQRS
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Add HTTP Context Accessor for authentication
builder.Services.AddHttpContextAccessor();

// Register the shared service for both server and client
builder.Services.AddScoped<GenealogyBlazorApp.Shared.Services.IPublicHomeService, GenealogyBlazorApp.Services.PublicHomeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

// Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GenealogyBlazorApp.Client._Imports).Assembly);

app.MapHomeEndpoints();

// Seed the database
await DataSeeder.SeedDataAsync(app);

app.Run();
