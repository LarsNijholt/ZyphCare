using ZyphCare.HealthRecords.Contracts;
using ZyphCare.HealthRecords.Entities;
using ZyphCare.HealthRecords.Extensions;

namespace ZyphCare.HealthRecords.Services;

/// <inheritdoc />
public sealed class HealthRecordBlobStorage : IHealthRecordBlobStorage
{
    /// <inheritdoc />
    public async Task WriteAsync(HealthRecord healthRecord, Stream data, CancellationToken cancellationToken = default)
    {
        var storageDirectory = healthRecord.GetStoragePath();
        Directory.CreateDirectory(storageDirectory);
        await using var fileStream = new FileStream(Path.Combine(storageDirectory, healthRecord.Id), FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
        await data.CopyToAsync(fileStream, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Stream?> ReadAsync(HealthRecord healthRecord, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(GetDefaultStorageDirectory(), healthRecord.GetStoragePath());

        if (!File.Exists(filePath))
        {
            return null;
        }

        var memoryStream = new MemoryStream();
        await using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            await fileStream.CopyToAsync(memoryStream, cancellationToken);
        }

        memoryStream.Position = 0;
        return memoryStream;
    }

    /// <inheritdoc />
    public void Delete(HealthRecord healthRecord)
    {
        var filePath = Path.Combine(GetDefaultStorageDirectory(), healthRecord.GetStoragePath());
        
        if(Directory.Exists(filePath))
            Directory.Delete(filePath, true);

        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    private static string GetDefaultStorageDirectory()
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ZyphCare", "Storage", "HealthRecords");
    }
}