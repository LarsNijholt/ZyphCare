using System.Security.Claims;

namespace ZyphCare.Web.Identity.Contracts;

/// <summary>
/// Defines methods for parsing JSON Web Tokens (JWTs) to extract claims.
/// </summary>
public interface IJwtParser
{
    /// <summary>
    /// Parses a JSON Web Token (JWT) and extracts a collection of claims.
    /// </summary>
    /// <param name="jwt">The JSON Web Token (JWT) string to be parsed.</param>
    /// <returns>A collection of claims extracted from the provided JWT.</returns>
    IEnumerable<Claim> Parse(string jwt);
}