using Refit;

namespace ZyphCare.Api.Client.Clients.HealthRecords.Requests;

/// <summary>
/// Represents a request to post a health record.
/// </summary>
public class PostHealthRecordRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the patient associated with the health record.
    /// </summary>
    public required string PatientId { get; set; }

    /// <summary>
    /// Gets or sets the name of the file associated with the health record.
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// Gets or sets the description of the health record being posted.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the type or category of the health record being posted.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the file associated with the health record request, used for uploading health-related documents.
    /// </summary>
    public StreamPart? File { get; set; }
}