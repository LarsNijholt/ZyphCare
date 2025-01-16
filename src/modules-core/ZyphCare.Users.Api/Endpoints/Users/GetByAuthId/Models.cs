using ZyphCare.Users.Models;

namespace ZyphCare.Users.Api.Endpoints.Users.GetByAuthId;

/// <summary>
/// Represents a request to retrieve a user by their unique identifier.
/// </summary>
public class Request
{
    /// <summary>
    /// Represents the unique identifier for the request or response object.
    /// </summary>
    public string AuthId { get; set; } = default!;
    
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

/// <summary>
/// Represents the response containing details of a user retrieved by their unique identifier.
/// </summary>
public record Response(
    string FirstName,
    string LastName,
    string Sex,
    DateTime DateOfBirth,
    string PhoneNumber,
    string AddressLine,
    string City,
    string PostalCode,
    string Country,
    BloodTypes? BloodType
);