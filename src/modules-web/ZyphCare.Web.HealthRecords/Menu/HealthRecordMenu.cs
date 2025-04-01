using Microsoft.AspNetCore.Components.Routing;
using ZyphCare.Web.Core.Contracts;
using ZyphCare.Web.Core.Models;

namespace ZyphCare.Web.HealthRecords.Menu;

/// <summary>
/// Add the health record menu.
/// </summary>
public class HealthRecordMenu : IMenuProvider
{
    /// <inheritdoc />
    public ValueTask<IEnumerable<MenuItem>> GetMenuItemsAsync(CancellationToken cancellationToken = default)
    {
        var menuItems = new List<MenuItem>
            {
                new()
                    {
                        Href = "/HealthRecords",
                        Text = "Health Records",
                        Order = 1,
                        Match = NavLinkMatch.All
                    }
            };

        return new ValueTask<IEnumerable<MenuItem>>(menuItems);
    }
}