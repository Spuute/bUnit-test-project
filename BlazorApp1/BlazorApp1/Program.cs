using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp1;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IApiService, ApiService>();
builder.Services.AddScoped<IClientErrorHandlingService, ClientErrorHandlingService>();
builder.Services.AddSingleton<AddPersonValidator>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();