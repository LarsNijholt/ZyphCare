using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using ZyphCare.Api.Client.Options;

namespace ZyphCare.Api.Client.HttpMessageHandlers;

/// <summary>
/// A custom HTTP message handler for adding an API key to the Authorization header of outgoing HTTP requests.
/// </summary>
/// <remarks>
/// This handler retrieves the API key from the provided <see cref="ZyphCareClientOptions"/> and includes it
/// in the Authorization header using the "ApiKey" scheme for each request.
/// </remarks>
public class ApiKeyHttpMessageHandler : DelegatingHandler
{
    private readonly ZyphCareClientOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiKeyHttpMessageHandler"/> class.
    /// </summary>
    public ApiKeyHttpMessageHandler(IOptions<ZyphCareClientOptions> options)
    {
        _options = options.Value;
    }

    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var apiKey = _options.ApiKey;
        request.Headers.Authorization = new AuthenticationHeaderValue("ApiKey", apiKey);

        return await base.SendAsync(request, cancellationToken);
    }
}