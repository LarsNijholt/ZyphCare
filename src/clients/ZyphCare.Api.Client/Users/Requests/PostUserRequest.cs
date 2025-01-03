namespace ZyphCare.Api.Client.Users.Requests;

/// <summary>
/// Represents a request to create or update a user record in the API.
/// </summary>
public class PostUserRequest
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The Id that is provided from auth 0.
    /// </summary>
    public string? Auth0Id { get; set; }

    /// <summary>
    /// The role assigned to the user.
    /// </summary>
    public string? Role { get; set; }
}