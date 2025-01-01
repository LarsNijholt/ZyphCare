using System.Security.Claims;

namespace ZyphCare.Web.Identity.Extensions;

public static class ClaimsExtensions
{
    public static bool IsExpired(this IEnumerable<Claim> claims)
    {
        var expString = claims.FirstOrDefault(x => x.Type == "exp")?.Value.Trim();
        var exp = !string.IsNullOrEmpty(expString) ? long.Parse(expString) : 0;
        var expiresAt = DateTimeOffset.FromUnixTimeSeconds(exp);
        return expiresAt < DateTimeOffset.UtcNow;
    }
}