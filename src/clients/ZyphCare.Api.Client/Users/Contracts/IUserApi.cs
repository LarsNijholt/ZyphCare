using Refit;
using ZyphCare.Api.Client.Shared.Models;
using ZyphCare.Api.Client.Users.Models;
using ZyphCare.Api.Client.Users.Requests;

namespace ZyphCare.Api.Client.Users.Contracts;

/// <summary>
/// Provides methods to interact with the user API for retrieving user data.
/// </summary>
public interface IUserApi
{
    /// <summary>
    /// Retrieves a paginated list of users based on the given filter criteria.
    /// </summary>
    /// <param name="request">The request containing filtering and pagination parameters for listing users.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated list response of users.</returns>
    [Get("/users")]
    Task<PagedListResponse<User>> ListAsync([Query] ListUserRequest request, CancellationToken cancellationToken = default);
    
    [Post("/users")]
    Task PostAsync(PostUserRequest request, CancellationToken cancellationToken = default);
}