using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using ZyphCare.Web.Identity.Contracts;
using ZyphCare.Web.Identity.Services;

namespace ZyphCare.Web.Identity.Pages;

[AllowAnonymous]
public partial class Login
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IAuthenticationProvider AuthenticationProvider { get; set; } = default!;
    [Inject] private IJwtAccessor JwtAccessor { get; set; } = default!;
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    /// <summary>
    /// The code retrieved from the Url.
    /// </summary>
    [Parameter]
    public string? Code { get; set; }
    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        var secret = Environment.GetEnvironmentVariable("AUTH0_SECRET");

        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("code", out var codeValue))
        {
            var jwt = await AuthenticationProvider.GetAccessTokenAsync(codeValue!, secret!);
            var tokens = JsonSerializer.Deserialize<JsonElement>(jwt);

            if (tokens.TryGetProperty("access_token", out var accessToken) && tokens.TryGetProperty("refresh_token", out var refreshToken))
            {
                await JwtAccessor.WriteTokenAsync("access_token", accessToken.GetString()!);
                await JwtAccessor.WriteTokenAsync("refresh_token", refreshToken.GetString()!);
            }
        }

        NavigationManager.NavigateTo("/");
        // ReSharper disable once SuspiciousTypeConversion.Global
        if (AuthenticationProvider is AccessTokenAuthenticationStateProvider accessTokenAuthenticationStateProvider)
        {
            accessTokenAuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }
    }
}