using Botick;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Botick.Identities;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomAuthorizationHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient("identity", opt => opt.BaseAddress = new Uri("https://localhost:5001"))
    .AddHttpMessageHandler<CustomAuthenticationMessageHandler>();
builder.Services.AddHttpClient("ServerAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7086");
}).AddHttpMessageHandler<CustomAuthorizationHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("identity"));
builder.Services.AddOidcAuthentication(opt =>
{
    opt.ProviderOptions.Authority = "https://localhost:5001";
    opt.ProviderOptions.ClientId = "blazor_webassembly";
    opt.ProviderOptions.ResponseType = "code";
    opt.ProviderOptions.DefaultScopes.Add("user");
    opt.ProviderOptions.DefaultScopes.Add("openid");
    opt.ProviderOptions.DefaultScopes.Add("api1");
});

await builder.Build().RunAsync();



