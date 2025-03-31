using ZyphCare.Web.Core.Models;

namespace ZyphCare.Web.Core.Contracts;

/// <summary>
/// Defines operations that manage and retrieve menu items for use within the application.
/// </summary>
public interface IMenuService
{
    /// <summary>
    /// Retrieves a collection of menu items asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>An asynchronous operation that yields a collection of menu items.</returns>
    ValueTask<IEnumerable<MenuItem>> GetMenuItemsAsync(CancellationToken cancellationToken = default);
}