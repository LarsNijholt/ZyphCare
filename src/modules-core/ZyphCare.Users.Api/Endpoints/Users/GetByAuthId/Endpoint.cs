using ZyphCare.Api.Common.Abstractions;
using ZyphCare.Users.Contracts;
using ZyphCare.Users.Filters;

namespace ZyphCare.Users.Api.Endpoints.Users.GetByAuthId;

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
        Get("/users/by-auth-id/{authId}");
        ConfigurePermissions("read:users");
    }

    /// <inheritdoc />
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var filter = new UserFilter { Auth0Id = req.AuthId };
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