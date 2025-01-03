using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ZyphCare.Web.Identity.Contracts;

namespace ZyphCare.Web.Identity.Services;

/// <inheritdoc />
public class JwtParser : IJwtParser
{
    /// <inheritdoc />
    public IEnumerable<Claim> Parse(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(jwt);
        return jwtSecurityToken.Claims;
    }
}