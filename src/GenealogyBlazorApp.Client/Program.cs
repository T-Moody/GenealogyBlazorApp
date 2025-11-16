using GenealogyBlazorApp.Client.Features.Authentication.Services;
using GenealogyBlazorApp.Shared.Features.Authentication.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configure HTTP client for API calls
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register WASM-side authentication service
builder.Services.AddScoped<IAuthenticationService, ClientAuthenticationService>();

await builder.Build().RunAsync();
