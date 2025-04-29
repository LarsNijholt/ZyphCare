namespace ZyphCare.Api.Client.Clients.Users.Requests.Users;

/// <summary>
/// Represents a request to create or update a user record in the API.
/// </summary>
public class PostUserRequest
{
    /// <summary>
    /// The Id that is provided from auth 0.
    /// </summary>
    public string Auth0Id { get; set; } = default!;

    /// <summary>
    /// The user's first name.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// The user's last name.
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Specifies the biological or self-identified sex of the user.
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
}