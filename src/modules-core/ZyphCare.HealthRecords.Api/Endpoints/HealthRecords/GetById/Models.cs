using ZyphCare.HealthRecords.Models;

namespace ZyphCare.HealthRecords.Api.Endpoints.HealthRecords.GetById;

/// <summary>
/// Represents the incoming request containing the identifier information needed to fetch specific health records data.
/// </summary>
public class Request
{
    /// <summary>
    /// Gets or sets the unique identifier used to fetch specific health records data.
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the unique identifier associated with a patient used to retrieve their specific health records.
    /// </summary>
    public string PatientId { get; set; } = default!;
}

/// <summary>
/// Represents a response detailing the health record information retrieved from the system.
/// </summary>
/// <param name="Id">The unique identifier of the health record.</param>
/// <param name="FileName">The name of the file associated with the health record.</param>
/// <param name="CreatedDate">The date and time when the health record was created.</param>
/// <param name="Type">The type of the health record.</param>
public record Response(string Id, string PatientId, string? FileName, DateTimeOffset CreatedDate, HealthRecordType Type);