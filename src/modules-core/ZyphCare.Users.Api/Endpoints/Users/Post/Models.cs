namespace ZyphCare.Users.Api.Endpoints.Users.Post;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
/// <remarks>
/// This class contains the necessary data for creating a user,
/// including the user's Auth0 ID and assigned role.
/// </remarks>
public class Request
{
    /// <summary>
    /// Gets or sets the unique identifier for the user's Auth0 account.
    /// </summary>
    /// <remarks>
    /// This identifier is critical for linking a user within the application to their corresponding Auth0 identity provider account,
    /// enabling seamless authentication and identity management.
    /// </remarks>
    public string Auth0Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the role assigned to the user.
    /// </summary>
    /// <remarks>
    /// This property defines the user's role within the application, which determines their permissions
    /// and access levels. It is a critical component for role-based access control and behavior customization
    /// in the system.
    /// </remarks>
    public string Role { get; set; } = default!;
}

