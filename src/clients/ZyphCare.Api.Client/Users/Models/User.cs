namespace ZyphCare.Api.Client.Users.Models;

/// <summary>
/// Represents a user entity with identification, authentication, and role information.
/// </summary>
public class User
{
    /// <summary>
    /// The unique identifier for the user.
    /// </summary>
    public string Id { get; set; } = default!;
    
    /// <summary>
    /// The Id that is provided from auth 0.
    /// </summary>
    public string Auth0Id { get; set; } = default!;

    /// <summary>
    /// The role of the user.
    /// </summary>
    public string Role { get; set; } = default!;
}