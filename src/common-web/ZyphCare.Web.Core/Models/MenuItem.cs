using Microsoft.AspNetCore.Components.Routing;

namespace ZyphCare.Web.Core.Models;

/// <summary>
/// Represents an item in a menu with associated properties and functionality.
/// </summary>
public class MenuItem
{
    /// <summary>
    /// Gets or sets the hyperlink reference for the menu item, specifying the URL or route to which the item should navigate.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// Gets or sets the matching behavior for the menu item's hyperlink, indicating whether it should match all or only the exact path.
    /// </summary>
    public NavLinkMatch Match { get; set; } = NavLinkMatch.All;

    /// <summary>
    /// Gets or sets the display text for the menu item, representing its label or title to be shown in the UI.
    /// </summary>
    public required string Text { get; set; }

    /// <summary>
    /// Gets or sets the order in which the menu item should be displayed relative to other items.
    /// </summary>
    public float Order { get; set; }
}