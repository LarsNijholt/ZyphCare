using ZyphCare.HealthRecords.Entities;

namespace ZyphCare.HealthRecords.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="HealthRecord"/> class, enabling additional
/// functionality related to health records in the ZyphCare system.
/// </summary>
public static class HealthRecordExtensions
{
    /// <summary>
    /// Generates the storage path for a given health record by combining the default
    /// storage directory path with the record's unique identifier.
    /// </summary>
    /// <param name="record">The health record for which the storage path is generated.</param>
    /// <returns>The full file system path where the health record should be stored.</returns>
    public static string GetStoragePath(this HealthRecord record)
    {
        return Path.Combine(GetDefaultStorageDirectory(), record.Type.ToString(), record.Id);
    }

    private static string GetDefaultStorageDirectory()
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ZyphCare", "Storage", "HealthRecords");
    }
}