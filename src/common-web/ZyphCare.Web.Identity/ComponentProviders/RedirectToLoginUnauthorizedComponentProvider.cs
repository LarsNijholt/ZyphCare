using Microsoft.AspNetCore.Components;
using ZyphCare.Web.Core.Contracts;
using ZyphCare.Web.Identity.Components;
using ZyphCare.Web.Identity.Extensions;

namespace ZyphCare.Web.Identity.ComponentProviders;

/// <summary>
/// Provides an implementation of the <see cref="IUnauthorizedComponentProvider"/>
/// interface to handle unauthorized access by rendering a component
/// that redirects users to the login page.
/// </summary>
public class RedirectToLoginUnauthorizedComponentProvider : IUnauthorizedComponentProvider
{

    /// <summary>
    /// Retrieves a RenderFragment that represents a component to be displayed
    /// when a user is unauthorized to access a specific resource or component.
    /// </summary>
    /// <returns>
    /// A RenderFragment rendering a <see cref="RedirectToLogin"/> component,
    /// which is responsible for redirecting unauthorized users to the login page.
    /// </returns>
    public RenderFragment GetUnauthorizedComponent()
    {
        return builder => builder.CreateComponent<RedirectToLogin>();
    }
}