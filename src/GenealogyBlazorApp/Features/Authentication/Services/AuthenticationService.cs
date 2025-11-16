using GenealogyBlazorApp.Domain.Entities;
using GenealogyBlazorApp.Features.Authentication.Commands;
using GenealogyBlazorApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GenealogyBlazorApp.Features.Authentication.Services;

public interface IAuthenticationService
{
    Task<LoginResult> AuthenticateAsync(string username, string password);
    Task SignOutAsync();
    Task<bool> IsAuthenticatedAsync();
}

public class AuthenticationService : IAuthenticationService
{
    private readonly GenealogyDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(GenealogyDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<LoginResult> AuthenticateAsync(string username, string password)
    {
        var user = await _dbContext.AdminUsers
            .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return new LoginResult(false, ErrorMessage: "Invalid username or password");
        }

        // Update last login
        user.LastLoginDate = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        // Create claims
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new("admin", "true")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        // Sign in
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8) // 8-hour session
                });
        }

        return new LoginResult(true);
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
}