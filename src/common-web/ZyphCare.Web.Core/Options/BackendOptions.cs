namespace ZyphCare.Web.Core.Options;

/// <summary>
/// Represents configuration options for connecting to a backend service.
/// </summary>
public class BackendOptions
{
    /// <summary>
    /// The URL of the backend.
    /// </summary>
    public Uri Url { get; set; } = default!;
}