using ZyphCare.HealthRecords.Entities;
using ZyphCare.HealthRecords.Models;

namespace ZyphCare.HealthRecords.Filters;

/// <summary>
/// Represents a filter used for querying health records within the system.
/// Provides various filter criteria such as record IDs, file names, creation dates, and types.
/// </summary>
public class HealthRecordFilter
{
    /// <summary>
    /// Gets or sets the unique identifier used to filter a specific health record.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the collection of unique identifiers used to filter health records.
    /// </summary>
    public ICollection<string>? Ids { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier associated with the patient.
    /// </summary>
    public string? PatientId { get; set; }

    /// <summary>
    /// Gets or sets a collection of patient identifiers used to filter health records associated with specific patients.
    /// </summary>
    public ICollection<string>? PatientIds { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the file associated with the health record.
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// Gets or sets the collection of file names associated with health records.
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
    /// Gets or sets the collection of health record types used to filter health records.
    /// </summary>
    public ICollection<HealthRecordType>? Types { get; set; }

    /// <summary>
    /// Applies the filter over the given queryable.
    /// </summary>
    public IQueryable<HealthRecord> Apply(IQueryable<HealthRecord> queryable)
    {
        if(Id != null)
            queryable = queryable.Where(x => x.Id == Id);
        if(Ids != null)
            queryable = queryable.Where(x => Ids.Contains(x.Id));
        if(PatientId != null)
            queryable = queryable.Where(x => x.PatientId == PatientId);
        if(PatientIds != null)
            queryable = queryable.Where(x => PatientIds.Contains(x.PatientId));
        if(FileName != null)
            queryable = queryable.Where(x => x.FileName == FileName);
        if(FileNames != null)
            queryable = queryable.Where(x => x.FileName != null && FileNames.Contains(x.FileName));
        if(CreatedDate != null)
            queryable = queryable.Where(x => x.CreatedDate == CreatedDate);
        if(Type != null)
            queryable = queryable.Where(x => x.Type == Type);
        if(Types != null)
            queryable = queryable.Where(x => Types.Contains(x.Type));
        return queryable;
    }
}