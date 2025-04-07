using System.Security.Claims;

namespace ZyphCare.Web.Identity.Extensions;

/// <summary>
/// Provides extension methods for working with claims in the context of security and identity.
/// </summary>
public static class ClaimsExtensions
{
    /// <summary>
    /// Determines whether the claims collection contains an expiration claim that has elapsed,
    /// indicating that the token or session is expired.
    /// </summary>
    /// <param name="claims">The collection of claims to evaluate.</param>
    /// <returns>True if the claims collection contains an "exp" claim, and the value indicates an
    /// expiration time earlier than the current time; otherwise, false.</returns>
    public static bool IsExpired(this IEnumerable<Claim> claims)
    {
        var expString = claims.FirstOrDefault(x => x.Type == "exp")?.Value.Trim();
        var exp = !string.IsNullOrEmpty(expString) ? long.Parse(expString) : 0;
        var expiresAt = DateTimeOffset.FromUnixTimeSeconds(exp);
        return expiresAt < DateTimeOffset.UtcNow;
    }
}