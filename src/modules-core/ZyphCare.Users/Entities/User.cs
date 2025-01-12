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
    /// The age of the user.
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// The blood type of the user, based on the ABO and Rh factor classification.
    /// </summary>
    public BloodTypes BloodType { get; set; }
}