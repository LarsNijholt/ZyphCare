using ZyphCare.Api.Client;
using ZyphCare.Web.Core.Contracts;

namespace ZyphCare.Web.Core.Services;

public class DefaultBackendApiClientProvider : IBackendApiClientProvider
{
    private readonly IRemoteBackendAccessor _remoteBackendAccessor;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultBackendApiClientProvider"/> class.
    /// </summary>
    public DefaultBackendApiClientProvider(IRemoteBackendAccessor remoteBackendAccessor, IServiceProvider serviceProvider)
    {
        _remoteBackendAccessor = remoteBackendAccessor;
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public Uri Url => _remoteBackendAccessor.RemoteBackend.Url;

    /// <inheritdoc />
    public ValueTask<T> GetApiAsync<T>(CancellationToken cancellationToken) where T : class
    {
        var backendUrl = _remoteBackendAccessor.RemoteBackend.Url;
        var client = _serviceProvider.CreateApi<T>(backendUrl);
        return new(client);
    }
}