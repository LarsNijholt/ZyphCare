using Refit;
using ZyphCare.Api.Client.Clients.HealthRecords.Models;
using ZyphCare.Api.Client.Clients.HealthRecords.Requests;
using ZyphCare.Api.Client.Shared.Models;

namespace ZyphCare.Api.Client.Clients.HealthRecords.Contracts;

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

    /// <summary>
    /// Creates a new health record based on the specified details.
    /// </summary>
    /// <param name="patientId">The unique identifier of the patient associated with the health record.</param>
    /// <param name="fileName">The name of the file associated with the health record.</param>
    /// <param name="description">The description of the health record being posted.</param>
    /// <param name="type">The type or category of the health record being posted.</param>
    /// <param name="file">The file associated with the health record request, used for uploading health-related documents.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The newly created health record.</returns>
    [Multipart]
    [Post("/health-records")]
    Task<HealthRecord> PostAsync(
        [AliasAs("patientId")] string patientId,
        [AliasAs("fileName")] string fileName,
        [AliasAs("description")] string? description,
        [AliasAs("type")] string? type,
        [AliasAs("file")] StreamPart? file,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a health record specified by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the health record to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests during the delete operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Delete("/health-records/{id}")]
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}