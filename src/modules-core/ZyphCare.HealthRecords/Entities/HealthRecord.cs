using ZyphCare.EntityFramework.Common.Entities;
using ZyphCare.HealthRecords.Models;

namespace ZyphCare.HealthRecords.Entities;

/// <summary>
/// Represents a health record entity in the system that manages health-related information.
/// </summary>
public class HealthRecord : VersionedEntity
{
    /// <summary>
    /// Gets or sets the unique identifier associated with the patient.
    /// </summary>
    public string PatientId { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the file associated with the health record.
    /// </summary>
    public string? FileName { get; set; }
    
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
    public DateTimeOffset ModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets the type of the health record, indicating its category or classification.
    /// </summary>
    public HealthRecordType Type { get; set; }
}