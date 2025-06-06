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