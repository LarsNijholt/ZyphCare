namespace ZyphCare.Api.Client.Clients.HealthRecords.Models;

/// <summary>
/// Model representing a health record.
/// </summary>
public class HealthRecord
{
    /// <summary>
    /// Gets or sets the name of the file associated with the health record.
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// The name of the patient.
    /// </summary>
    public string PatientName { get; set; } = default!;
    
    /// <summary>
    /// A short description about what the health record is about.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the health record was created.
    /// </summary>
    public DateTimeOffset CreatedDate { get; set; }
    
    /// <summary>
    /// The date the health record was last modified.
    /// </summary>
    public DateTimeOffset? ModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets the type of the health record, indicating its category or classification.
    /// </summary>
    public string? Type { get; set; }
}