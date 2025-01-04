using ZyphCare.Api.Common.Abstractions;

namespace ZyphCare.Api.Common.Endpoints;

/// <summary>
/// Simple endpoint that returns data.
/// </summary>
public class HealthCheck : ZyphCareEndpoint<Request, Response>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Get("/health");
        ConfigureRoles("admin");
    }

    /// <inheritdoc />
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = new Response
            {
                Health = "Healthy",
            };
        await SendOkAsync(response, ct);
    }
}