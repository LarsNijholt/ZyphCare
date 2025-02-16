using ZyphCare.Api.Common.Abstractions;
using ZyphCare.HealthRecords.Contracts;
using ZyphCare.HealthRecords.Filters;

namespace ZyphCare.HealthRecords.Api.Endpoints.GetById;

/// <summary>
/// Endpoint that gets a health record by its Id.
/// </summary>
public class Endpoint : ZyphCareEndpoint<Request, Response, Mapper>
{
    private readonly IHealthRecordEntityStore _store;

    /// <inheritdoc />
    public Endpoint(IHealthRecordEntityStore store)
    {
        _store = store;
    }
    
    /// <inheritdoc />
    public override void Configure()
    {
        Get("/health-records/{id}");
        ConfigureRoles("Doctor, Patient");
    }

    /// <inheritdoc />
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var healthRecordFilter = new HealthRecordFilter
            {
                Id = req.Id,
                PatientId = req.PatientId
            };
        var healthRecord = await _store.FindAsync(healthRecordFilter, cancellationToken: ct);

        if (healthRecord == null)
        {
            await SendNotFoundAsync(ct);
            return;   
        }
        var response = Map.FromEntity(healthRecord);
        await SendAsync(response, cancellation: ct);
    }
}