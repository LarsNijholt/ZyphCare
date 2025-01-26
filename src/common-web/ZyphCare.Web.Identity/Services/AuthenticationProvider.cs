using System.Text.Json;
using Microsoft.Extensions.Options;
using ZyphCare.Web.Identity.Contracts;
using ZyphCare.Web.Identity.Options;

namespace ZyphCare.Web.Identity.Services;

/// <inheritdoc />
public class AuthenticationProvider : IAuthenticationProvider
{
    private readonly string _secret;
    
    /// <summary>
    /// Provides an implementation for authentication operations by
    /// interacting with an external authentication service to retrieve access tokens.
    /// </summary>
    public AuthenticationProvider(IOptions<ZyphCareIdentityOptions> zyphCareIdentityOptions)
    {
        _secret = zyphCareIdentityOptions.Value.Secret ?? string.Empty;
    }

    /// <inheritdoc />
    public async Task<string> GetAccessTokenAsync(string code)
    {
        var client = new HttpClient();
        var requestContent = new FormUrlEncodedContent([
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", "uYjZgtRicwlVkRR6WW9N3DqHw8C7EmAT"),
                new KeyValuePair<string, string>("client_secret", _secret),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", "https://localhost:7184/callback")
            ]);

        var response = await client.PostAsync("https://dev-g2gar2vb3zazyj0s.us.auth0.com/oauth/token", requestContent);
        return await response.Content.ReadAsStringAsync();
    }
    
    /// <inheritdoc />
    public async Task<string?> GetAccessTokenByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var requestBody = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "client_id", "uYjZgtRicwlVkRR6WW9N3DqHw8C7EmAT" },
                { "client_secret", _secret},
                { "refresh_token", refreshToken }
            };

        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Post, "https://dev-g2gar2vb3zazyj0s.us.auth0.com/oauth/token");
        request.Content = new FormUrlEncodedContent(requestBody);

        var response = await client.SendAsync(request, cancellationToken);
        var token = await response.Content.ReadAsStringAsync(cancellationToken);
        
        var tokenJson = JsonDocument.Parse(token);
        return tokenJson.RootElement.TryGetProperty("access_token", out var accessToken) ? accessToken.GetString() : null;
    }
}