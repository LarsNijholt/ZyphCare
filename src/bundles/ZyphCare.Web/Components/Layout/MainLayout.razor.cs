using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Navigations;
using ZyphCare.Web.Models;

namespace ZyphCare.Web.Components.Layout;

public partial class MainLayout
{
    private const ExpandAction Expand = ExpandAction.Click;
    private bool _sidebarToggle;
    private readonly Dictionary<string, object> _htmlAttribute = new() { { "class", "sidebar-treeview" } };
    private readonly List<TreeData> _treeData = [];
    private void Toggle(MouseEventArgs args)
    {
        _sidebarToggle = !_sidebarToggle;
    }

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        _treeData.Add(new TreeData { NodeId = "02", NodeText = "Deployment", IconCss = "icon-annotation-edit icon", NavigateUrl = "/counter" });
        _treeData.Add(new TreeData { NodeId = "03", NodeText = "Quick Start", IconCss = "icon-docs icon", NavigateUrl = "/" });
    }
}