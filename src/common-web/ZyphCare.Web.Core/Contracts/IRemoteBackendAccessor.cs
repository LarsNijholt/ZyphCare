using ZyphCare.Web.Core.Models;

namespace ZyphCare.Web.Core.Contracts;

public interface IRemoteBackendAccessor
{
    /// <summary>
    /// Gets or sets the current backend.
    /// </summary>
    RemoteBackend RemoteBackend { get; }
}