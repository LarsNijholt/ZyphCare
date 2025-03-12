using Microsoft.AspNetCore.Components.Routing;
using ZyphCare.Web.Core.Contracts;
using ZyphCare.Web.Core.Models;

namespace ZyphCare.Studio.Dashboard.Menu;

/// <summary>
/// Add the dashboard menu.
/// </summary>
public class DashboardMenu : IMenuProvider
{
    /// <inheritdoc />
    public ValueTask<IEnumerable<MenuItem>> GetMenuItemsAsync()
    {
        var menuItems = new List<MenuItem>
            {
                new()
                    {
                        Href = "/",
                        Text = "Dashboard",
                        Order = 1,
                        Match = NavLinkMatch.All
                    }
            };

        return new ValueTask<IEnumerable<MenuItem>>(menuItems);
    }
}