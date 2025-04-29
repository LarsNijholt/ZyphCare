namespace ZyphCare.Api.Client.Clients.HealthRecords.Requests;

/// <summary>
/// Represents a request for listing health records with various filtering options.
/// </summary>
public class ListHealthRecordRequest
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
    /// The identifier associated with the patient.
    /// </summary>
    public string? PatientId { get; set; }

    /// <summary>
    /// A collection of patient identifiers used to filter health records.
    /// </summary>
    public ICollection<string>? PatientIds { get; set; }
    
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
    public int? Type { get; set; }
    
    /// <summary>
    /// The page number.
    /// </summary>
    public int? Page { get; init; }

    /// <summary>
    /// The amount of users to display on the page.
    /// </summary>
    public int? PageSize { get; init; }
}