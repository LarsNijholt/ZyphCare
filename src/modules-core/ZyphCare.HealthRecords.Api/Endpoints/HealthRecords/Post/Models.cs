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