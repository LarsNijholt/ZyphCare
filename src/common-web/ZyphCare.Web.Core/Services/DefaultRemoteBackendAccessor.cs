using Microsoft.Extensions.Options;
using ZyphCare.Web.Core.Contracts;
using ZyphCare.Web.Core.Models;
using ZyphCare.Web.Core.Options;

namespace ZyphCare.Web.Core.Services;

/// <inheritdoc />
public class DefaultRemoteBackendAccessor : IRemoteBackendAccessor
{

    private readonly IOptions<BackendOptions> _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultRemoteBackendAccessor"/> class.
    /// </summary>
    public DefaultRemoteBackendAccessor(IOptions<BackendOptions> options)
    {
        _options = options;
    }

    /// <inheritdoc />
    public RemoteBackend RemoteBackend => new(_options.Value.Url);
}