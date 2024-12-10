using ZyphCare.EntityFramework.Common.Entities;

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
    /// The role of the user.
    /// </summary>
    public string Role { get; set; } = default!;
}