using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GenealogyBlazorApp.Shared.Services;
using GenealogyBlazorApp.Client.Features.Home.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IPublicHomeService, PublicHomeService>();
builder.Services.AddScoped<IHomeService, HomeService>();

await builder.Build().RunAsync();
