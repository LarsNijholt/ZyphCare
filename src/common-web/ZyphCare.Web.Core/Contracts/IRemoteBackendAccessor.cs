using ZyphCare.Web.Core.Models;

namespace ZyphCare.Web.Core.Contracts;

/// <summary>
/// Interface for accessing a remote backend.
/// Provides a contract for retrieving the configuration or properties of a remote backend service.
/// </summary>
public interface IRemoteBackendAccessor
{
    /// <summary>
    /// Gets or sets the current backend.
    /// </summary>
    RemoteBackend RemoteBackend { get; }
}