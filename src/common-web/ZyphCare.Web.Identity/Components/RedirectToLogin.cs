using Microsoft.AspNetCore.Components;

namespace ZyphCare.Web.Identity.Components;

/// <summary>
/// Represents a Blazor component used to redirect users to the login page if unauthorized.
/// </summary>
public class RedirectToLogin : ComponentBase
{
    /// <summary>
    /// Gets or sets the <see cref="NavigationManager"/>.
    /// </summary>
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    /// <inheritdoc />
    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        NavigationManager.NavigateTo("SignIn", true);
        return Task.CompletedTask;
    }
}