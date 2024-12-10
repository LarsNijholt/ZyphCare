using ZyphCare.Users.Entities;

namespace ZyphCare.Users.Filters;

/// <summary>
/// Filter for filtering <see cref="User"/>.
/// </summary>
public class UserFilter
{
    /// <summary>
    /// The Id of the filter.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The Ids of the filter.
    /// </summary>
    public ICollection<string>? Ids { get; set; }

    /// <summary>
    /// The Id that is provided from auth0.
    /// </summary>
    public string? Auth0Id { get; set; }

    /// <summary>
    /// The Id that is provided from auth0.
    /// </summary>
    public ICollection<string>? Auth0Ids { get; set; }

    /// <summary>
    /// The role of the user.
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// The role of the user.
    /// </summary>
    public ICollection<string>? Roles { get; set; }

    /// <summary>
    /// Applies the filter to the specified queryable.
    /// </summary>
    /// <param name="queryable">The queryable to apply the filter to.</param>
    /// <returns>The filtered results.</returns>
    public IQueryable<User> Apply(IQueryable<User> queryable)
    {
        if (Id != null)
            queryable = queryable.Where(x => x.Id == Id);
        if (Ids != null) 
            queryable = queryable.Where(x => Ids.Contains(x.Id));
        if (Auth0Id != null)
            queryable = queryable.Where(x => x.Auth0Id == Auth0Id);
        if (Auth0Ids != null) 
            queryable = queryable.Where(x => Auth0Ids.Contains(x.Auth0Id));
        if (Role != null)
            queryable = queryable.Where(x => x.Role == Role);
        if (Roles != null) 
            queryable = queryable.Where(x => Roles.Contains(x.Role));
        
        return queryable;
    }
}