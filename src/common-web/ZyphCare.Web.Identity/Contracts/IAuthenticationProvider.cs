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
    Task<string> GetAccessTokenAsync(string code);

    /// <summary>
    /// Asynchronously retrieves a new access token using the provided refresh token.
    /// </summary>
    /// <param name="refreshToken">
    /// The refresh token used to obtain a new access token.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous operation, containing the new access token as a string
    /// if successful, or null if the token could not be retrieved.
    /// </returns>
    Task<string?> GetAccessTokenByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}