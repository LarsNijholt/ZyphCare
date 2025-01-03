namespace ZyphCare.Users.Api.Endpoints.Users.GetById;

/// <summary>
/// Represents a request to retrieve a user by their unique identifier.
/// </summary>
public class Request
{
    /// <summary>
    /// Represents the unique identifier for the request or response object.
    /// </summary>
    public string Id { get; set; } = default!;
}

/// <summary>
/// Represents the response containing details of a user retrieved by their unique identifier.
/// </summary>
public record Response(string Id, string Auth0Id, string Role);