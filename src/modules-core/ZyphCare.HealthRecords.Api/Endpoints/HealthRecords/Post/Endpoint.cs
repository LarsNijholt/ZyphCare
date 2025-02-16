using ZyphCare.Api.Common.Abstractions;
using ZyphCare.HealthRecords.Contracts;
using ZyphCare.HealthRecords.Entities;

namespace ZyphCare.HealthRecords.Api.Endpoints.HealthRecords.Post;

/// <summary>
/// Defines an API endpoint for posting health records to the system.
/// </summary>
public class Endpoint : ZyphCareEndpoint<Request, Response, Mapper>
{
    private readonly IHealthRecordBlobStorage _healthRecordBlobStorage;
    private readonly IHealthRecordEntityStore _healthRecordEntityStore;

    /// <inheritdoc />
    public Endpoint(IHealthRecordBlobStorage healthRecordBlobStorage, IHealthRecordEntityStore healthRecordEntityStore)
    {
        _healthRecordBlobStorage = healthRecordBlobStorage;
        _healthRecordEntityStore = healthRecordEntityStore;
    }
    
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/health-records");
        ConfigureRoles("Doctor");
        AllowFileUploads();
    }
    
    /// <inheritdoc />
    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var healthRecord = CreateNewHealthRecord(request);
        await using var stream = request.File.OpenReadStream();
        stream.Position = 0;
        await _healthRecordBlobStorage.WriteAsync(healthRecord, stream, cancellationToken);

        await _healthRecordEntityStore.SaveAsync(healthRecord, cancellationToken);
        var response = await Map.FromEntityAsync(healthRecord, cancellationToken);
        await SendCreatedAtAsync<GetById.Endpoint>(healthRecord.Id, response, cancellation: cancellationToken);
    }

    private static HealthRecord CreateNewHealthRecord(Request request)
    {
        return new HealthRecord
            {
                Id = Guid.NewGuid().ToString(),
                FileName = request.Name,
                Type = request.Type,
                CreatedDate = DateTimeOffset.Now
            };
    }
}