using Refit;
using ZyphCare.Api.Client.HealthRecords.Models;
using ZyphCare.Api.Client.HealthRecords.Requests;
using ZyphCare.Api.Client.Shared.Models;

namespace ZyphCare.Api.Client.HealthRecords.Contracts;

/// <summary>
/// Defines the contract for accessing health record resources in the API.
/// </summary>
public interface IHealthRecordApi
{
    /// <summary>
    /// Fetches a paged list of health records based on the specified filtering options.
    /// </summary>
    /// <param name="request">The request object containing filtering and pagination parameters for retrieving health records.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    [Get("/health-records")]
    Task<PagedListResponse<HealthRecord>> ListAsync([Query] ListHealthRecordRequest request, CancellationToken cancellationToken = default);
}