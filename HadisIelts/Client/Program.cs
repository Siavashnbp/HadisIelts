using HadisIelts.Client;
using HadisIelts.Client.RequestHandlers.Account.Services;
using HadisIelts.Client.Services.Authorization;
using HadisIelts.Client.Services.File;
using HadisIelts.Client.Services.Writing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("HadisIelts.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddHttpClient("HadisIelts.AnonymousAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));


// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("HadisIelts.ServerAPI"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddApiAuthorization().AddAccountClaimsPrincipalFactory<CustomUserFactory>();

builder.Services.AddScoped<IPasswordService, PasswordServiceProvider>();
builder.Services.AddScoped<IFileServices, FileServiceProvider>();
builder.Services.AddScoped<IClientWritingServices, ClientWritingServiceProvider>();

await builder.Build().RunAsync();
