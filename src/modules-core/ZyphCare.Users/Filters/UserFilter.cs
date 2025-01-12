using ZyphCare.Users.Entities;
using ZyphCare.Users.Models;

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
    /// The Ids that are provided from auth0.
    /// </summary>
    public ICollection<string>? Auth0Ids { get; set; }

    /// <summary>
    /// The first name to filter by.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The last name to filter by.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// The sex of the user to filter by.
    /// </summary>
    public string? Sex { get; set; }

    /// <summary>
    /// The minimum age to filter by.
    /// </summary>
    public int? MinAge { get; set; }

    /// <summary>
    /// The maximum age to filter by.
    /// </summary>
    public int? MaxAge { get; set; }

    /// <summary>
    /// The blood type to filter by.
    /// </summary>
    public BloodTypes? BloodType { get; set; }

    /// <summary>
    /// Applies the filter to the specified queryable.
    /// </summary>
    /// <param name="queryable">The queryable to apply the filter to.</param>
    /// <returns>The filtered queryable.</returns>
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
        if (FirstName != null)
            queryable = queryable.Where(x => x.FirstName.Contains(FirstName));
        if (LastName != null)
            queryable = queryable.Where(x => x.LastName.Contains(LastName));
        if (Sex != null)
            queryable = queryable.Where(x => x.Sex == Sex);
        if (MinAge.HasValue)
            queryable = queryable.Where(x => x.Age >= MinAge);
        if (MaxAge.HasValue)
            queryable = queryable.Where(x => x.Age <= MaxAge);
        if (BloodType.HasValue)
            queryable = queryable.Where(x => x.BloodType == BloodType.Value);

        return queryable;
    }
}