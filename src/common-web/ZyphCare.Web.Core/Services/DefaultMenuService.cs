using ZyphCare.Web.Core.Contracts;
using ZyphCare.Web.Core.Models;

namespace ZyphCare.Web.Core.Services;

/// <inheritdoc />
public class DefaultMenuService : IMenuService
{
    private readonly IEnumerable<IMenuProvider> _menuProviders;

    /// <summary>
    /// Default implementation of the <see cref="IMenuService"/> interface, responsible for collating and ordering
    /// menu items provided by multiple <see cref="IMenuProvider"/> instances.
    /// </summary>
    public DefaultMenuService(IEnumerable<IMenuProvider> menuProviders)
    {
        _menuProviders = menuProviders;
    }

    /// <inheritdoc />
    public async ValueTask<IEnumerable<MenuItem>> GetMenuItemsAsync(CancellationToken cancellationToken = default)
    {
        var menu = new List<MenuItem>();

        foreach (var menuProvider in _menuProviders)
        {
            var menuItems = await menuProvider.GetMenuItemsAsync(cancellationToken);
            menu.AddRange(menuItems);
        }

        return menu.OrderBy(x => x.Order).ToList();
    }
}