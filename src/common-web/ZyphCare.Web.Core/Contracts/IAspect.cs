namespace ZyphCare.Web.Core.Contracts;

/// <summary>
/// An Aspect for front end modules.
/// </summary>
public interface IAspect
{
    /// <summary>
    /// Initializes the aspect asynchronously. This method is typically used to perform
    /// any required setup operations for the aspect.
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used to cancel the initialization operation.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask"/> representing the asynchronous operation.
    /// </returns>
    ValueTask InitializeAsync(CancellationToken cancellationToken = default);
}