using ZyphCare.Api.Common.Abstractions;
using ZyphCare.Api.Common.Models;
using ZyphCare.Common.Models;
using ZyphCare.Users.Contracts;
using ZyphCare.Users.Filters;

namespace ZyphCare.Users.Api.Endpoints.Users.List;

/// <summary>
/// Represents an endpoint for retrieving a paginated list of users from the system.
/// It processes the request, applies the necessary permissions, and provides a response containing the user data.
/// </summary>
public class Endpoint : ZyphCareEndpoint<Request, PagedListResponse<Response>, Mapper>
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
        Get("/users/list");
        ConfigurePermissions("read:users");
    }

    /// <inheritdoc />
    public override async Task<PagedListResponse<Response>> ExecuteAsync(Request request, CancellationToken ct)
    { 
        var pageArgs = PageArgs.FromPage(request.Page, request.PageSize);
        var userFilter = CreateUserFilter(request);
        var userResponse = await _userEntityStore.FindManyAsync(userFilter, pageArgs, ct);
        
        var users = userResponse.Items.Select(x => Map.FromEntity(x));
        var pagedResponse = new Page<Response>(users.ToList(), userResponse.TotalCount);

        return new PagedListResponse<Response>(pagedResponse);
    }

    private UserFilter CreateUserFilter(Request request)
    {
        return new UserFilter
            {
                Id = request.Id,
                Ids = request.Ids,
                Auth0Id = request.Auth0Id,
                Auth0Ids = request.Auth0Ids,
                Role = request.Role,
                Roles = request.Roles,
            };
    }
}