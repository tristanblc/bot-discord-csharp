using BotBlazonApplication;
using BotBlazonApplication.Services.Interface;
using BotBlazonApplication.Services.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton<ILocalStorage, LocalStorage>(); // NOTE: this line is newly added-
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7167/api/") });
builder.Services.AddSingleton< IAuthService, AuthService>();

await builder.Build().RunAsync();
await builder.Build().RunAsync();
