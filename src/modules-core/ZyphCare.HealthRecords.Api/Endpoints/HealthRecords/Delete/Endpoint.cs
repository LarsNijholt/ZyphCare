using ZyphCare.Api.Common.Abstractions;
using ZyphCare.HealthRecords.Contracts;
using ZyphCare.HealthRecords.Filters;

namespace ZyphCare.HealthRecords.Api.Endpoints.HealthRecords.Delete;

/// <summary>
/// Represents an endpoint for handling delete operations on health records.
/// This class extends the <see cref="ZyphCareEndpoint{TRequest}"/> class,
/// specifically utilizing the <see cref="Request"/> model.
/// </summary>
public class Endpoint : ZyphCareEndpoint<Request>
{
    private readonly IHealthRecordEntityStore _store;
    private readonly IHealthRecordBlobStorage _healthRecordBlobStorage;

    /// <inheritdoc />
    public Endpoint(IHealthRecordEntityStore store, IHealthRecordBlobStorage healthRecordBlobStorage)
    {
        _store = store;
        _healthRecordBlobStorage = healthRecordBlobStorage;
    }
    
    /// <inheritdoc />
    public override void Configure()
    {
        Delete("health-records/{id}");
        ConfigureRoles("Doctor");
    }

    /// <inheritdoc />
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var filter = new HealthRecordFilter {Id = req.Id, PatientId = req.PatientId};
        var healthRecord = await _store.FindAsync(filter, ct);

        if(healthRecord == null)
            return;
        
        _healthRecordBlobStorage.Delete(healthRecord);
        await _store.DeleteAsync(healthRecord, ct);

        await SendOkAsync(ct);
    }
}