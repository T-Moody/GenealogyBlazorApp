using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GenealogyBlazorApp.Shared.Services;
using GenealogyBlazorApp.Client.Features.Home.Services;
using GenealogyBlazorApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAdminHomeService, AdminHomeService>();
builder.Services.AddScoped<IPublicHomeService, PublicHomeService>();

await builder.Build().RunAsync();
