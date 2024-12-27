namespace ZyphCare.Api.Client.Options;

/// <summary>
/// Configuration options for setting up the ZyphCare API client.
/// </summary>
public class ZyphCareClientOptions
{
    /// <summary>
    /// Gets or sets the base address for the ZyphCare API client.
    /// This property specifies the root URI that serves as the starting point for all API requests.
    /// </summary>
    public Uri BaseAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets the API key used for authenticating requests to the ZyphCare API.
    /// This property allows the client to securely identify and authorize its requests.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets a delegate that configures the <see cref="HttpClient"/> instance used by the ZyphCare API client.
    /// This can be used to modify default settings or add custom headers and handlers for the HTTP client.
    /// </summary>
    public Action<IServiceProvider, HttpClient>? ConfigureHttpClient { get; set; }
}