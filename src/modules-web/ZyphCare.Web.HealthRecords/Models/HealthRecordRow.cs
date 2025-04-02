namespace ZyphCare.Web.HealthRecords.Models;

/// <summary>
/// Record representing a HealthRecord in a grid row.
/// </summary>
public record HealthRecordRow(string FileName, string Patient, string LastModified, string CreatedDate, string Type);