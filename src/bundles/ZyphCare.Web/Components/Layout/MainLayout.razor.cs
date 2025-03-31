using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using ZyphCare.Web.Core.Contracts;
using ZyphCare.Web.Identity.Extensions;

namespace ZyphCare.Web.Components.Layout;

public partial class MainLayout
{
    private bool _drawerIsOpen = true;
    private ErrorBoundary? _errorBoundary;
    
    [Inject] private IAspectService AspectService { get; set; } = null!;

    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        if (AuthenticationState != null)
        {
            var authState = await AuthenticationState;
            if (authState.User.Identity?.IsAuthenticated == true && !authState.User.Claims.IsExpired())
            {
                await AspectService.InitializeAspectsAsync();
                StateHasChanged();
            }
        }
        else
        {
            await AspectService.InitializeAspectsAsync();
            StateHasChanged();
        }
        
    }

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        _errorBoundary?.Recover();
    }
    
    private void ToggleDrawer()
    {
        _drawerIsOpen = !_drawerIsOpen;
    }
}