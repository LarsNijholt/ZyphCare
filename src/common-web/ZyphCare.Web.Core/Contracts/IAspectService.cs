namespace ZyphCare.Web.Core.Contracts;

/// <summary>
/// Defines a service that manages and initializes aspects for front-end modules.
/// This interface provides methods to retrieve and initialize aspects as well
/// as an event to notify when the initialization process is complete.
/// </summary>
public interface IAspectService
{
    /// <summary>
    /// An event that is triggered when the aspect service is initialized.
    /// This event can be used by subscribers to perform actions after
    /// the service has completed its initialization process.
    /// </summary>
    event Action? Initialized;

    /// <summary>
    /// Retrieves the collection of features represented as aspects. This method
    /// provides access to all the aspects available in the system.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{IAspect}"/> containing the available aspects.
    /// </returns>
    IEnumerable<IAspect> GetAspects();

    /// <summary>
    /// Initializes all aspects asynchronously. This method performs the required setup
    /// operations for the front-end modules managed by the service.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used to cancel the initialization process.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous initialization operation.
    /// </returns>
    Task InitializeAspectsAsync(CancellationToken cancellationToken = default);
}