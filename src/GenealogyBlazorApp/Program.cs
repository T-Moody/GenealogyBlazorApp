using GenealogyBlazorApp.Client.Pages;
using GenealogyBlazorApp.Components;
using GenealogyBlazorApp.Features.Authentication.Authorization;
using GenealogyBlazorApp.Features.Authentication.Endpoints;
using GenealogyBlazorApp.Features.Authentication.Services;
using GenealogyBlazorApp.Features.Counties.Endpoints;
using GenealogyBlazorApp.Infrastructure.Data;
using GenealogyBlazorApp.Shared.Features.Authentication.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Configure Entity Framework
builder.Services.AddDbContext<GenealogyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/admin/login";
        options.LogoutPath = "/admin/logout";
        options.AccessDeniedPath = "/admin/login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

// Configure Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AuthorizationPolicies.AdminOnly, policy =>
        policy.Requirements.Add(new AdminRequirement()));
});

// Register Authorization Handlers
builder.Services.AddScoped<IAuthorizationHandler, AdminAuthorizationHandler>();

// Configure MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Register Authentication Services
builder.Services.AddScoped<IAuthenticationDomainService, AuthenticationDomainService>();
builder.Services.AddScoped<IAuthenticationService, ServerAuthenticationService>();

// Add HTTP Context Accessor for authentication
builder.Services.AddHttpContextAccessor();

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

// Map API endpoints
app.MapAuthenticationEndpoints();
app.MapCountiesEndpoints();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GenealogyBlazorApp.Client._Imports).Assembly);

// Ensure database is created and seeded in development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<GenealogyDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
