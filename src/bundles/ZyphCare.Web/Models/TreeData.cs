namespace ZyphCare.Web.Models;

/// <summary>
/// Class representing data in the nav menu.
/// </summary>
public class TreeData
{
    /// <summary>
    /// The NodeId.
    /// </summary>
    public string NodeId { get; set; }
    
    /// <summary>
    /// The Node Text.
    /// </summary>
    public string NodeText { get; set; }
    
    /// <summary>
    /// The css of the icon.
    /// </summary>
    public string IconCss { get; set; }
    
    /// <summary>
    /// If the entry has children.
    /// </summary>
    public bool HasChild { get; set; }
    
    /// <summary>
    /// The Pid of the tree data.
    /// </summary>
    public string Pid { get; set; }
    
    /// <summary>
    /// The Url to navigate to.
    /// </summary>
    public string NavigateUrl { get; set; }
}