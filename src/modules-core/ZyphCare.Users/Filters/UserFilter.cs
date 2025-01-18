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
    /// The date of birth of the user.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>
    /// The user's phone number.
    /// </summary>
    public string? PhoneNumber { get; set; } = default!;

    /// <summary>
    /// The street address information associated with the user.
    /// </summary>
    public string? AddressLine { get; set; } = default!;

    /// <summary>
    /// The city in which the user resides.
    /// </summary>
    public string? City { get; set; } = default!;
    
    public ICollection<string>? Cities { get; set; } = default!;

    /// <summary>
    /// The postal code associated with the user's address.
    /// </summary>
    public string? PostalCode { get; set; } = default!;
    
    public ICollection<string>? PostalCodes { get; set; } = default!;

    /// <summary>
    /// The country associated with the user's address.
    /// </summary>
    public string? Country { get; set; } = default!;

    public ICollection<string>? Countries { get; set; } = default!;
    
    /// <summary>
    /// The blood type to filter by.
    /// </summary>
    public BloodTypes? BloodType { get; set; }

    /// <summary>
    /// Collection of blood types used as a filter criterion for users.
    /// </summary>
    public ICollection<BloodTypes>? BloodTypes { get; set; }

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
        if (BloodType.HasValue)
            queryable = queryable.Where(x => x.BloodType == BloodType.Value);
        if(BloodTypes != null)
            queryable = queryable.Where(x => x.BloodType != null && BloodTypes.Contains(x.BloodType.Value));
        if (DateOfBirth.HasValue)
            queryable = queryable.Where(x => x.DateOfBirth == DateOfBirth.Value);
        if (!string.IsNullOrWhiteSpace(PhoneNumber))
            queryable = queryable.Where(x => x.PhoneNumber == PhoneNumber);
        if (!string.IsNullOrWhiteSpace(AddressLine))
            queryable = queryable.Where(x => x.AddressLine.Contains(AddressLine));
        if (!string.IsNullOrWhiteSpace(City))
            queryable = queryable.Where(x => x.City.Contains(City));
        if (!string.IsNullOrWhiteSpace(PostalCode))
            queryable = queryable.Where(x => x.PostalCode == PostalCode);
        if (!string.IsNullOrWhiteSpace(Country))
            queryable = queryable.Where(x => x.Country == Country);

        return queryable;
    }
}