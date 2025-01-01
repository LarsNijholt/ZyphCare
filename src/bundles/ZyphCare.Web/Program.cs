using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using ZyphCare.Web.Components;
using MudBlazor.Services;
using Syncfusion.Blazor;
using ZyphCare.Studio.Dashboard.Extensions;
using ZyphCare.Web.Core.Constants;
using ZyphCare.Web.Core.Extensions;
using ZyphCare.Web.Core.Models;
using ZyphCare.Web.Extensions;
using ZyphCare.Web.Handlers;
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
    .AddStudioDashboard()
    .AddCascadingAuthenticationState()
    .AddRemoteBackend(backendApiConfig)
    .AddIdentityServices();

services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "DefaultScheme";
        options.DefaultChallengeScheme = "DefaultScheme";
    })
    .AddScheme<AuthenticationSchemeOptions, DefaultAuthenticationHandler>("DefaultScheme", _ => { });

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
    var serviceProvider = httpContext.RequestServices;
    var jwtAccessor = serviceProvider.GetRequiredService<IJwtAccessor>();

    await jwtAccessor.RemoveTokenAsync(TokenNames.AccessToken);
    await jwtAccessor.RemoveTokenAsync(TokenNames.RefreshToken);
    
    var navigationManager = serviceProvider.GetRequiredService<NavigationManager>();
    navigationManager.NavigateTo("/SignIn");
});

app.UseAuthorization();
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
var conventionBuilder = app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.AddRouting(conventionBuilder);

app.Run();
