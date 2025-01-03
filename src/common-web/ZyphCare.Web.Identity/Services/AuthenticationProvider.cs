using ZyphCare.Web.Identity.Contracts;

namespace ZyphCare.Web.Identity.Services;

/// <inheritdoc />
public class AuthenticationProvider : IAuthenticationProvider
{
    /// <inheritdoc />
    public async Task<string> GetAccessTokenAsync(string code, string secret)
    {
        var client = new HttpClient();
        var requestContent = new FormUrlEncodedContent([
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", "uYjZgtRicwlVkRR6WW9N3DqHw8C7EmAT"),
                new KeyValuePair<string, string>("client_secret", $"{secret}"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", "https://localhost:7184/callback")
            ]);

        var response = await client.PostAsync("https://dev-g2gar2vb3zazyj0s.us.auth0.com/oauth/token", requestContent);
        return await response.Content.ReadAsStringAsync();
    }
}