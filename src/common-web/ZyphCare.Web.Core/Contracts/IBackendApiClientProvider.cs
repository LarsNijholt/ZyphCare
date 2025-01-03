namespace ZyphCare.Web.Core.Contracts;

/// <summary>
/// Defines a provider for accessing backend API clients.
/// </summary>
public interface IBackendApiClientProvider
{
    /// <summary>
    /// Gets the URL to the backend.
    /// </summary>
    Uri Url { get; }

    /// <summary>
    /// Gets an API client from the backend connection provider.
    /// </summary>
    /// <typeparam name="T">The API client type.</typeparam>
    /// <returns>The API client.</returns>
    ValueTask<T> GetApiAsync<T>(CancellationToken cancellationToken = default) where T : class;
}