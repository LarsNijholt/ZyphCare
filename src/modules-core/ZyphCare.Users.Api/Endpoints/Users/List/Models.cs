namespace ZyphCare.Users.Api.Endpoints.Users.List;

/// <summary>
/// Represents a request containing parameters for filtering user information.
/// </summary>
public class Request
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// A collection of unique identifiers for filtering user records.
    /// </summary>
    public ICollection<string>? Ids { get; set; }

    /// <summary>
    /// The Id that is provided from auth 0.
    /// </summary>
    public string? Auth0Id { get; set; }

    /// <summary>
    /// A collection of identifiers associated with users in Auth0.
    /// </summary>
    public ICollection<string>? Auth0Ids { get; set; }

    /// <summary>
    /// The role assigned to the user, representing their permissions or access level within the system.
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ICollection<string>? Roles { get; set; }
    
    /// <summary>
    /// The page number.
    /// </summary>
    public int? Page { get; init; }

    /// <summary>
    /// The amount of users to display on the page.
    /// </summary>
    public int? PageSize { get; init; }
}

/// <summary>
/// Represents a response containing user information.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="Auth0Id">The identifier associated with the user in Auth0.</param>
/// <param name="Role">The role assigned to the user.</param>
public record Response(string Id, string Auth0Id, string Role);