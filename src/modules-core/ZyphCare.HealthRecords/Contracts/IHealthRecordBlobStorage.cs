using ZyphCare.HealthRecords.Entities;

namespace ZyphCare.HealthRecords.Contracts;

/// <summary>
/// Interface that provides blob storage functionality for health records.
/// </summary>
public interface IHealthRecordBlobStorage
{
    /// <summary>
    /// Writes the provided health record data to blob storage.
    /// </summary>
    /// <param name="healthRecord">
    /// The health record to associate with the stored data, containing metadata like file name, type, and created date.
    /// </param>
    /// <param name="data">
    /// A stream representing the data to be saved associated with the given health record.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests, allowing the operation to be cancelled if necessary.
    /// Defaults to <see cref="CancellationToken.None"/> if not provided.
    /// </param>
    Task WriteAsync(HealthRecord healthRecord, Stream data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the stored health record data from blob storage.
    /// </summary>
    /// <param name="healthRecord">
    /// The health record associated with the stored data, containing metadata like file name, type, and created date.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests, allowing the operation to be cancelled if necessary.
    /// Defaults to <see cref="CancellationToken.None"/> if not provided.
    /// </param>
    Task<Stream?> ReadAsync(HealthRecord healthRecord, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified health record and its associated data from blob storage.
    /// </summary>
    /// <param name="healthRecord">
    /// The health record whose data is to be deleted, containing metadata like file name, type, and created date.
    /// </param>
    void Delete(HealthRecord healthRecord);
}