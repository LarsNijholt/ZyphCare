using ZyphCare.EntityFramework.Common.Entities;
using ZyphCare.Users.Models;

// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

namespace ZyphCare.Users.Entities;

/// <summary>
/// An Entity representing the user object.
/// </summary>
public class User : Entity
{
    /// <summary>
    /// The Id that is provided from auth 0.
    /// </summary>
    public string Auth0Id { get; set; } = default!;

    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// The last name of the user.
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Represents the gender of the user.
    /// </summary>
    public string Sex { get; set; } = default!;

    /// <summary>
    /// The date of birth of the user.
    /// </summary>
    public DateTime DateOfBirth { get; set; }
    
    /// <summary>
    /// The user's phone number.
    /// </summary>
    public string PhoneNumber { get; set; } = default!;

    /// <summary>
    /// The street address information associated with the user.
    /// </summary>
    public string AddressLine { get; set; } = default!;

    /// <summary>
    /// The city in which the user resides.
    /// </summary>
    public string City { get; set; } = default!;

    /// <summary>
    /// The postal code associated with the user's address.
    /// </summary>
    public string PostalCode { get; set; } = default!;

    /// <summary>
    /// The country associated with the user's address.
    /// </summary>
    public string Country { get; set; } = default!;

    /// <summary>
    /// The blood type of the user, based on the ABO and Rh factor classification.
    /// </summary>
    public BloodTypes? BloodType { get; set; }
}