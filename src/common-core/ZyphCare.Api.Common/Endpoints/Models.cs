namespace ZyphCare.Api.Common.Endpoints;

/// <summary>
/// Represents a request model containing data payload.
/// </summary>
public class Request
{
    /// <summary>
    /// Gets or sets the data payload contained within the request.
    /// This property may be null if no data is provided with the request.
    /// </summary>
    public string? Data { get; set; }    
}

/// <summary>
/// Represents a response model containing the health status information.
/// </summary>
public class Response
{
    /// <summary>
    /// Gets or sets the health status of the application or service.
    /// This property represents the current operational status and provides a
    /// textual indicator such as "Healthy" or other descriptive status values.
    /// </summary>
    public string Health { get; set; } = "Healthy";
}