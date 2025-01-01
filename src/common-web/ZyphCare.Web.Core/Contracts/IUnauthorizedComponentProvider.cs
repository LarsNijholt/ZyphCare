using Microsoft.AspNetCore.Components;

namespace ZyphCare.Web.Core.Contracts;

/// <summary>
/// Defines a contract for providing RenderFragment components to be displayed
/// when a user is unauthorized to access a specific resource or component.
/// </summary>
public interface IUnauthorizedComponentProvider
{
    /// <summary>
    /// Retrieves the RenderFragment component to be displayed when a user
    /// is unauthorized to access a specific resource or component.
    /// </summary>
    /// <returns>
    /// A RenderFragment that represents the component to display for unauthorized access.
    /// </returns>
    RenderFragment GetUnauthorizedComponent();
}