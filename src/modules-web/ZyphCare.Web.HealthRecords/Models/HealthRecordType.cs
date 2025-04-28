namespace ZyphCare.Web.HealthRecords.Models;

/// <summary>
/// The type of the HealthRecord.
/// </summary>
public enum HealthRecordType
{
    /// <summary>
    /// Represents a health record type specifically for invoices related to healthcare services.
    /// </summary>
    Invoice = 0,

    /// <summary>
    /// Represents a health record type specifically for laboratory test results, detailing medical test outcomes and analyses.
    /// </summary>
    LabResult = 1,

    /// <summary>
    /// Represents a health record type focused on consultations, including notes and discussions between patients and healthcare providers.
    /// </summary>
    Consultation = 2
}