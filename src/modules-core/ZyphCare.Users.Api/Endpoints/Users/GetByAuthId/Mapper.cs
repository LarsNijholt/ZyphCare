using FastEndpoints;
using ZyphCare.Users.Entities;

namespace ZyphCare.Users.Api.Endpoints.Users.GetByAuthId;

/// <summary>
/// A class responsible for mapping a <see cref="User"/> entity to a <see cref="Response"/> DTO in the context of getting user details by their unique identifier.
/// </summary>
/// <remarks>
/// This class inherits from <see cref="ResponseMapper{TResponse,TEntity}"/> and overrides the method to transform a User entity into a corresponding Response object.
/// </remarks>
public class Mapper : ResponseMapper<Response, User>
{
    /// <inheritdoc />
    public override Task<Response> FromEntityAsync(User user, CancellationToken ct = new CancellationToken()) => Task.FromResult(new Response(
    user.Id,
    user.Auth0Id));
}