using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Navigations;
using ZyphCare.Web.Models;

namespace ZyphCare.Web.Components.Layout;

public partial class MainLayout
{
    private bool _drawerIsOpen = true;
    private ErrorBoundary? _errorBoundary;
}