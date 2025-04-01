using Microsoft.AspNetCore.Authentication;
using ZyphCare.Web.Components;
using MudBlazor.Services;
using Syncfusion.Blazor;
using ZyphCare.Studio.Dashboard.Extensions;
using ZyphCare.Studio.Dashboard.HttpMessageHandler;
using ZyphCare.Web.Core.Extensions;
using ZyphCare.Web.Core.Models;
using ZyphCare.Web.Extensions;
using ZyphCare.Web.Handlers;
using ZyphCare.Web.HealthRecords.Extensions;
using ZyphCare.Web.Identity.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var license = builder.Configuration.GetSection("syncfusion")["license"];
var auth0Secret = builder.Configuration.GetRequiredSection("auth0");
var backendApiConfig = new BackendApiConfig
    {
        ConfigureBackendOptions = options => configuration.GetSection("Backend").Bind(options),
        ConfigureHttpClientBuilder = options =>
        {
            options.AuthenticationHandler = typeof(AuthenticatingApiHttpMessageHandler);
        }
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
    .AddHealthRecords()
    .AddCascadingAuthenticationState()
    .AddRemoteBackend(backendApiConfig)
    .AddIdentityServices(options => auth0Secret.Bind(options));

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
}

app.UseAuthorization();
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
var conventionBuilder = app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.AddRouting(conventionBuilder);

app.Run();
