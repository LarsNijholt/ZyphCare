using ZyphCare.EntityFramework.Common.Entities;

namespace ZyphCare.HealthRecords.Entities;

/// <summary>
/// Represents a health record entity in the system that manages health-related information.
/// </summary>
public class HealthRecord : Entity
{
    /// <summary>
    /// Gets or sets the name of the file associated with the health record.
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the health record was created.
    /// </summary>
    public DateTimeOffset CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the type of the health record, indicating its category or classification.
    /// </summary>
    public string? Type { get; set; }
}