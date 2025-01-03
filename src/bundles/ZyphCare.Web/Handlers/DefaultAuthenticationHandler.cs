using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ZyphCare.Web.Handlers;

/// <summary>
/// A custom authentication handler that implements the default authentication logic for an application.
/// Provides a mechanism for authenticating requests using a predefined identity and authentication scheme.
/// </summary>
public class DefaultAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{

    /// <inheritdoc />
    public DefaultAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    /// <inheritdoc />
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new GenericIdentity("DefaultIdentity")), "DefaultAuthenticationScheme")));
    }
}