using ZyphCare.Web.Core.Models;

namespace ZyphCare.Web.Core.Contracts;

/// <summary>
/// An interface for providing new modules to the menu.
/// </summary>
public interface IMenuProvider
{
    /// <summary>
    /// Asynchronously retrieves a collection of menu items.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an enumerable collection of menu items.
    /// </returns>
    ValueTask<IEnumerable<MenuItem>> GetMenuItemsAsync(CancellationToken cancellationToken = default);
}