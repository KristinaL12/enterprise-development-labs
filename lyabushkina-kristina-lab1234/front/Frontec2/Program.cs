using Frontec2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

/// <summary>
/// Точка входа клиентского Blazor WASM-приложения.
/// Здесь настраиваются сервисы (такие как HttpClient), задается ip
/// и запускается главный компонент.
/// </summary>

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5262/") });

await builder.Build().RunAsync();
