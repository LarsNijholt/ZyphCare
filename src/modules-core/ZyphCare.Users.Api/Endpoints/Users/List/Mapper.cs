using FastEndpoints;
using ZyphCare.Users.Entities;

namespace ZyphCare.Users.Api.Endpoints.Users.List;

/// <summary>
/// Provides mapping functionality between a User entity and a Response object.
/// </summary>
public class Mapper : ResponseMapper<Response, User>
{
    /// <summary>
    /// Maps a <see cref="User"/> entity to a <see cref="Response"/> object.
    /// </summary>
    /// <param name="user">The user entity to be mapped.</param>
    /// <returns>A <see cref="Response"/> object containing the mapped properties of the user.</returns>
    public override Response FromEntity(User user) => new(user.Id, user.Auth0Id);
}