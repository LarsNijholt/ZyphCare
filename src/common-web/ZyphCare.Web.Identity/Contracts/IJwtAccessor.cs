namespace ZyphCare.Web.Identity.Contracts;

/// <summary>
/// Provides functionality to manage JWT tokens, including reading, writing, and deleting tokens asynchronously.
/// </summary>
public interface IJwtAccessor
{
    /// <summary>
    /// Reads a token asynchronously based on the provided name.
    /// </summary>
    /// <param name="name">The name associated with the token to be read.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the token as a string, or null if no token is found.</returns>
    ValueTask<string?> ReadTokenAsync(string name);

    /// <summary>
    /// Writes a token asynchronously with the specified name and value.
    /// </summary>
    /// <param name="name">The name associated with the token to be written.</param>
    /// <param name="token">The token value to be stored.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask WriteTokenAsync(string name, string token);

    /// <summary>
    /// Removes a token asynchronously based on the provided name.
    /// </summary>
    /// <param name="name">The name associated with the token to be removed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask RemoveTokenAsync(string name);
}