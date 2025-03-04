namespace ZyphCare.HealthRecords.Api.Endpoints.HealthRecords.Delete;

/// <summary>
/// Represents a request for deleting a health record.
/// </summary>
public class Request
{
    /// <summary>
    /// Gets or sets the unique identifier for the health record to be deleted.
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the unique identifier for the patient associated with the health record to be deleted.
    /// </summary>
    public string PatientId { get; set; } = default!;
}