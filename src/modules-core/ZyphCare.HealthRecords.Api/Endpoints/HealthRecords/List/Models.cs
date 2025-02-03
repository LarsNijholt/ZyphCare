using ZyphCare.HealthRecords.Models;

namespace ZyphCare.HealthRecords.Api.Endpoints.HealthRecords.List;

/// <summary>
/// Represents a request for retrieving health records with optional filtering criteria.
/// </summary>
public class Request
{
    /// <summary>
    /// The id of the health record.
    /// </summary>
    public string? Id { get; set; }
    
    /// <summary>
    /// A collection of unique identifiers for filtering user records.
    /// </summary>
    public ICollection<string>? Ids { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the file associated with the health record.
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// A collection of file names associated with the health records.
    /// </summary>
    public ICollection<string>? FileNames { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the health record was created.
    /// </summary>
    public DateTimeOffset? CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the type of the health record, indicating its category or classification.
    /// </summary>
    public HealthRecordType? Type { get; set; }
    
    /// <summary>
    /// The page number.
    /// </summary>
    public int? Page { get; init; }

    /// <summary>
    /// The amount of users to display on the page.
    /// </summary>
    public int? PageSize { get; init; }
}

/// <summary>
/// Represents a response detailing the health record information retrieved from the system.
/// </summary>
/// <param name="Id">The unique identifier of the health record.</param>
/// <param name="FileName">The name of the file associated with the health record.</param>
/// <param name="CreatedDate">The date and time when the health record was created.</param>
/// <param name="Type">The type of the health record.</param>
public record Response(string Id, string? FileName, DateTimeOffset CreatedDate, HealthRecordType Type);
