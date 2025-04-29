using Refit;
using ZyphCare.Api.Client.Clients.Users.Models;
using ZyphCare.Api.Client.Clients.Users.Requests.Users;
using ZyphCare.Api.Client.Shared.Models;

namespace ZyphCare.Api.Client.Clients.Users.Contracts;

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

    /// <summary>
    /// Retrieves a user record based on the provided Auth0 identifier.
    /// </summary>
    /// <param name="authId">The Auth0 identifier of the user to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    [Get("/users/by-auth-id/{authId}")]
    Task<IApiResponse<User?>> GetWithAuth0IdAsync([Query] string authId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Submits a request to create a new user with the specified details.
    /// </summary>
    /// <param name="request">The request containing the information required to create a user.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    [Post("/users")]
    Task PostAsync(PostUserRequest request, CancellationToken cancellationToken = default);
}