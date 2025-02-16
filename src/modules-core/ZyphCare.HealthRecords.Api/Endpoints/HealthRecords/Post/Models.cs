using Microsoft.AspNetCore.Http;
using ZyphCare.HealthRecords.Models;

namespace ZyphCare.HealthRecords.Api.Endpoints.HealthRecords.Post;

/// <summary>
/// Represents a request model for posting health records.
/// </summary>
public class Request
{
    /// <summary>
    /// Gets or sets the name provided in the health record request, which is commonly used to label or identify the record.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the type of the health record, which indicates the classification or category of the record, such as invoice, lab result, or consultation.
    /// </summary>
    public HealthRecordType Type { get; set; }

    /// <summary>
    /// Represents a file sent with the HTTP request, typically used for handling file uploads.
    /// This property holds the uploaded file as an instance of <see cref="Microsoft.AspNetCore.Http.IFormFile"/>.
    /// </summary>
    public IFormFile File { get; set; } = default!;
}

/// <summary>
/// Represents the response model containing details of a health record.
/// </summary>
/// <param name="Id">The unique identifier for the health record.</param>
/// <param name="PatientId">The unique identifier for the patient associated with the health record.</param>
/// <param name="FileName">The name of the file associated with the health record, if any.</param>
/// <param name="CreatedDate">The date and time the health record was created.</param>
/// <param name="Type">The type of the health record.</param>
public record Response(string Id, string PatientId, string? FileName, DateTimeOffset CreatedDate, HealthRecordType Type);