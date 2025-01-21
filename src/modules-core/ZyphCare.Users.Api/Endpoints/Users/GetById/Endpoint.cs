using ZyphCare.Api.Common.Abstractions;
using ZyphCare.Users.Contracts;
using ZyphCare.Users.Filters;

namespace ZyphCare.Users.Api.Endpoints.Users.GetById;

/// <summary>
/// Endpoint for retrieving user details by their unique identifier.
/// </summary>
/// <remarks>
/// The <see cref="Endpoint"/> is a specialization of <see cref="ZyphCareEndpoint{TRequest, TResponse, TMapper}"/> used to handle HTTP GET requests for user details.
/// It depends on an implementation of <see cref="IUserEntityStore"/> to retrieve user information from a datastore.
/// </remarks>
public class Endpoint : ZyphCareEndpoint<Request, Response, Mapper>
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
        Get("/users/{id}");
        Roles("user");
    }

    /// <inheritdoc />
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var filter = new UserFilter { Id = req.Id, };
        var user = await _userEntityStore.FindAsync(filter, ct);

        if (user == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        var response = await Map.FromEntityAsync(user, ct);
        
        await SendOkAsync(response, ct);
    }
}