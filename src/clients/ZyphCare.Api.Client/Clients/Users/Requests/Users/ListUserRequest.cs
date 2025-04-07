namespace ZyphCare.Api.Client.Clients.Users.Requests.Users;

/// <summary>
/// Represents a request for listing users with the option to filter by various criteria.
/// </summary>
public class ListUserRequest
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