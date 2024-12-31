namespace ZyphCare.Web.Identity.Contracts;

/// <summary>
/// Defines the contract for an authentication provider capable of retrieving access tokens
/// for the purpose of authenticating and authorizing client requests.
/// </summary>
public interface IAuthenticationProvider
{
    /// <summary>
    /// Asynchronously retrieves an access token for authentication purposes.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation, containing the access token as a string
    /// if successful, or null if the token could not be retrieved.
    /// </returns>
    Task<string> GetAccessTokenAsync(string code, string secret);
}