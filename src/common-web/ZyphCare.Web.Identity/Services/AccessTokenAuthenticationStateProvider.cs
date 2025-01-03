using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using ZyphCare.Web.Core.Constants;
using ZyphCare.Web.Identity.Contracts;
using ZyphCare.Web.Identity.Extensions;

namespace ZyphCare.Web.Identity.Services;

/// <summary>
/// Provides an implementation of <see cref="AuthenticationStateProvider"/> that retrieves
/// authentication state based on an access token. This class interacts with JSON Web Tokens (JWTs)
/// for managing and validating user authentication by leveraging <see cref="IJwtAccessor"/> and <see cref="IJwtParser"/>.
/// </summary>
public class AccessTokenAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IJwtAccessor _jwtAccessor;
    private readonly IJwtParser _jwtParser;

    /// <inheritdoc />
    public AccessTokenAuthenticationStateProvider(IJwtAccessor jwtAccessor, IJwtParser jwtParser)
    {
        _jwtAccessor = jwtAccessor;
        _jwtParser = jwtParser;
    }

    /// <inheritdoc />
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authToken = await _jwtAccessor.ReadTokenAsync(TokenNames.AccessToken);

        if (string.IsNullOrEmpty(authToken))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        
        var claims = _jwtParser.Parse(authToken).ToList();
        var isExpired = claims.IsExpired();
        
        if(isExpired)
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    /// <summary>
    /// Notifies components that the authentication state has changed, invoking the provided
    /// asynchronous method to retrieve the updated <see cref="AuthenticationState"/>.
    /// </summary>
    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}