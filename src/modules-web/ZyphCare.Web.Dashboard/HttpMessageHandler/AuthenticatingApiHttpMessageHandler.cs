using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Web.Core.Constants;
using ZyphCare.Web.Core.Contracts;
using ZyphCare.Web.Identity.Contracts;

namespace ZyphCare.Studio.Dashboard.HttpMessageHandler;

/// <summary>
/// A custom HTTP message handler that manages authentication for outgoing HTTP requests by attaching
/// a Bearer token to the Authorization header, refreshing the token if necessary, and retrying the request.
/// </summary>
public class AuthenticatingApiHttpMessageHandler : DelegatingHandler
{
    private readonly IBlazorServiceAccessor _serviceAccessor;
    private readonly IAuthenticationProvider _authenticationProvider;

    /// <summary>
    /// A custom HTTP message handler that attaches a Bearer token to the Authorization header for outgoing HTTP requests.
    /// It utilizes dependency-injected services for retrieving tokens and handles scenarios such as token refresh and request retries.
    /// </summary>
    public AuthenticatingApiHttpMessageHandler(IBlazorServiceAccessor serviceAccessor,  IAuthenticationProvider authenticationProvider)
    {
        _serviceAccessor = serviceAccessor;
        _authenticationProvider = authenticationProvider;
    }
   
    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var jwtAccessor = _serviceAccessor.Services.GetRequiredService<IJwtAccessor>();
        var accessToken = await jwtAccessor.ReadTokenAsync(TokenNames.AccessToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            // Refresh token and retry once.
            var token = await RefreshTokenAsync(jwtAccessor, cancellationToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Retry.
            response = await base.SendAsync(request, cancellationToken);
        }

        return response;
    }

    private async Task<string?> RefreshTokenAsync(IJwtAccessor jwtAccessor, CancellationToken cancellationToken)
    {
        // Get refresh token.
        var refreshToken = await jwtAccessor.ReadTokenAsync(TokenNames.RefreshToken);

        if (refreshToken == null)
            return null;

        var accessToken = await _authenticationProvider.GetAccessTokenByRefreshTokenAsync(refreshToken, cancellationToken);
        
        if(accessToken == null)
            return null;
        
        // Store token.
        await jwtAccessor.WriteTokenAsync(TokenNames.AccessToken, accessToken);
        
        // Return tokens.
        return accessToken;
    }
}