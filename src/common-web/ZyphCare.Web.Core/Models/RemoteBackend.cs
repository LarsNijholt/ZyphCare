namespace ZyphCare.Web.Core.Models;

/// <summary>
/// Represents a remote backend service with a specified URL.
/// </summary>
public class RemoteBackend
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RemoteBackend"/> class.
    /// </summary>
    /// <param name="url">The URL of the backend.</param>
    public RemoteBackend(Uri url)
    {
        Url = url;
    }

    /// <summary>
    /// The URL of the backend.
    /// </summary>
    public Uri Url { get; }
}