using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ZyphCare.Web.Components;
using MudBlazor.Services;
using Syncfusion.Blazor;
using ZyphCare.Web.Core.Constants;
using ZyphCare.Web.Core.Extensions;
using ZyphCare.Web.Core.Models;
using ZyphCare.Web.Extensions;
using ZyphCare.Web.Identity.Contracts;
using ZyphCare.Web.Identity.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var license = builder.Configuration.GetSection("syncfusion")["license"];
var auth0Secret = builder.Configuration.GetSection("auth0")["secret"];
var backendApiConfig = new BackendApiConfig
    {
        ConfigureBackendOptions = options => configuration.GetSection("Backend").Bind(options),
    };


Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(license);

services
    .AddSyncfusionBlazor()
    .AddRazorComponents()
    .AddInteractiveServerComponents();

services.AddServerSideBlazor();
services
    .AddMudServices()
    .AddCore()
    .AddCascadingAuthenticationState()
    .AddRemoteBackend(backendApiConfig)
    .AddIdentityServices()
    .AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = configuration["Auth0:Domain"] ?? string.Empty;
        options.ClientId = configuration["Auth0:ClientId"] ?? string.Empty;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    Environment.SetEnvironmentVariable("AUTH0_SECRET", auth0Secret);
}

app.MapGet("/Account/Logout", async httpContext =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
        .WithRedirectUri("/")
        .Build();

    await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    
    var serviceProvider = httpContext.RequestServices;
    var jwtAccessor = serviceProvider.GetRequiredService<IJwtAccessor>();

    await jwtAccessor.RemoveTokenAsync(TokenNames.AccessToken);
    await jwtAccessor.RemoveTokenAsync(TokenNames.RefreshToken);
});

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
var conventionBuilder = app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.AddRouting(conventionBuilder);

app.Run();
