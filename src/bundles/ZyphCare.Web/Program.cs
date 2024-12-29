using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using ZyphCare.Web.Components;
using MudBlazor.Services;
using Syncfusion.Blazor;
using ZyphCare.Api.Client.Users.Contracts;
using ZyphCare.Api.Client.Users.Requests;
using ZyphCare.Web.Core.Extensions;
using ZyphCare.Web.Core.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var license = builder.Configuration.GetSection("syncfusion")["license"];

services.AddSyncfusionBlazor();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(license);

services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

services.AddServerSideBlazor();

var backendApiConfig = new BackendApiConfig
    {
        ConfigureBackendOptions = options => configuration.GetSection("Backend").Bind(options),
    };

services
    .AddMudServices()
    .AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = configuration["Auth0:Domain"] ?? string.Empty;
        options.ClientId = configuration["Auth0:ClientId"] ?? string.Empty;
    });

services.AddRemoteBackend(backendApiConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/Account/Login", async (HttpContext httpContext, string returnUrl = "/") =>
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
        .WithRedirectUri(returnUrl)
        .Build();
    
    await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    
    var user = httpContext.User;
    var auth0Id = user.FindFirstValue(ClaimTypes.NameIdentifier);
    var userApi = httpContext.RequestServices.GetRequiredService<IUserApi>();
    var request = new PostUserRequest { Id = Guid.NewGuid().ToString(), Auth0Id = auth0Id };
    await userApi.PostAsync(request);
});

app.MapGet("/Account/Logout", async httpContext =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
        .WithRedirectUri("/")
        .Build();

    await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
