using ZyphCare.Users.Models;

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

