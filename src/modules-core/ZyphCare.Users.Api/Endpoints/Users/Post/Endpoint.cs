using ZyphCare.Api.Common.Abstractions;
using ZyphCare.Users.Contracts;
using ZyphCare.Users.Entities;
using ZyphCare.Users.Filters;

namespace ZyphCare.Users.Api.Endpoints.Users.Post;

/// <summary>
/// Handles the HTTP POST endpoint for creating a new user entity in the system.
/// </summary>
/// <remarks>
/// This endpoint is responsible for validating incoming user creation requests. It ensures that a user
/// with the provided Auth0 ID does not already exist. If the user exists, an error response is returned.
/// Otherwise, a new user entity is created and persisted using the IUserEntityStore interface.
/// </remarks>
public class Endpoint : ZyphCareEndpoint<Request>
{
    private readonly IUserEntityStore _userEntityStore;

    /// <inheritdoc />
    public Endpoint(IUserEntityStore userEntityStore)
    {
        _userEntityStore = userEntityStore;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("/users");
        AllowAnonymous();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var filter = new UserFilter { Auth0Id = req.Auth0Id };

        var exists = await _userEntityStore.AnyAsync(filter, ct);
        if (exists)
        {
            AddError("User already exists.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Auth0Id = req.Auth0Id,
            };

        await _userEntityStore.SaveAsync(user, ct);
        await SendOkAsync("User created.", ct);
    }
}