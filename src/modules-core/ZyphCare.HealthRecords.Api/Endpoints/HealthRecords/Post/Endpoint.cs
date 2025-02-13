using ZyphCare.Api.Common.Abstractions;

namespace ZyphCare.HealthRecords.Api.Endpoints.HealthRecords.Post;

/// <summary>
/// Defines an API endpoint for posting health records to the system.
/// </summary>
public class Endpoint : ZyphCareEndpoint<Request>
{
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
        ;
    }
}