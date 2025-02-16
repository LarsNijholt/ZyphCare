using ZyphCare.Api.Common.Abstractions;
using ZyphCare.Api.Common.Models;
using ZyphCare.Common.Models;
using ZyphCare.HealthRecords.Contracts;
using ZyphCare.HealthRecords.Filters;

namespace ZyphCare.HealthRecords.Api.Endpoints.HealthRecords.List;

/// <summary>
/// Represents an API endpoint for handling operations related to health records,
/// specifically for listing health records based on given criteria.
/// </summary>
public class Endpoint : ZyphCareEndpoint<Request, PagedListResponse<Response>, Mapper>
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
        Get("/health-records");
        ConfigureRoles("User", "Doctor");
    }

    /// <inheritdoc />
    public override async Task<PagedListResponse<Response>> ExecuteAsync(Request request, CancellationToken ct)
    {
        var pageArgs = PageArgs.FromPage(request.Page, request.PageSize);
        var healthRecordFilter = CreateHealthRecordFilter(request);
        var response = await _store.FindManyAsync(healthRecordFilter, pageArgs, ct);

        var healthRecords = response.Items.Select(x => Map.FromEntity(x)).ToList();
        var pagedResponse = new Page<Response>(healthRecords.ToList(), response.TotalCount);
        return new PagedListResponse<Response>(pagedResponse);
    }

    private HealthRecordFilter CreateHealthRecordFilter(Request request)
    {
        return new HealthRecordFilter
            {
                Id = request.Id,
                Ids = request.Ids,
                PatientId = request.PatientId,
                PatientIds = request.PatientIds,
                CreatedDate = request.CreatedDate,
                FileName = request.FileName,
                FileNames = request.FileNames,
            };
    }
}