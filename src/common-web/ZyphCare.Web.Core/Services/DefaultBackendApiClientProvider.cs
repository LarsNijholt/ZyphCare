using System.Reflection;
using ZyphCare.Api.Client;
using ZyphCare.Api.Client.Extensions;
using ZyphCare.Web.Core.Contracts;

namespace ZyphCare.Web.Core.Services;

/// <inheritdoc />
public class DefaultBackendApiClientProvider : IBackendApiClientProvider
{
    private readonly IRemoteBackendAccessor _remoteBackendAccessor;
    private readonly IBlazorServiceAccessor _blazorServiceAccessor;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultBackendApiClientProvider"/> class.
    /// </summary>
    public DefaultBackendApiClientProvider(IRemoteBackendAccessor remoteBackendAccessor, IBlazorServiceAccessor blazorServiceAccessor, IServiceProvider serviceProvider)
    {
        _remoteBackendAccessor = remoteBackendAccessor;
        _blazorServiceAccessor = blazorServiceAccessor;
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public Uri Url => _remoteBackendAccessor.RemoteBackend.Url;

    /// <inheritdoc />
    public ValueTask<T> GetApiAsync<T>(CancellationToken cancellationToken) where T : class
    {
        var backendUrl = _remoteBackendAccessor.RemoteBackend.Url;
        var client = _serviceProvider.CreateApi<T>(backendUrl);
        var decorator = DispatchProxy.Create<T, BlazorScopedProxyApi<T>>();
        (decorator as BlazorScopedProxyApi<T>)!.Initialize(client, _blazorServiceAccessor, _serviceProvider);
        return new(decorator);
    }
}