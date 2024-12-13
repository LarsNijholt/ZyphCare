namespace ZyphCare.Aspects.Models;

/// <summary>
/// Represents a descriptor for an aspect, including its name, namespace,
/// display name, description, and fully qualified name.
/// </summary>
public class AspectDescriptor
{
    /// <summary>
    /// Represents a descriptor for an aspect, providing metadata such as the name,
    /// namespace, display name, description, and fully qualified name.
    /// </summary>
    public AspectDescriptor()
    {
    }

    /// <summary>
    /// Represents a descriptor for an aspect, providing properties like name, namespace,
    /// display name, description, and the ability to construct the fully qualified name.
    /// </summary>
    public AspectDescriptor(string name, string ns, string displayName, string? description = default)
    {
        Name = name;
        Namespace = ns;
        DisplayName = displayName;
        Description = description ?? "";
    }
    
    /// <summary>
    /// Gets or sets the name of the aspect.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the namespace of the aspect.
    /// </summary>
    public string Namespace { get; set; } = default!;

    /// <summary>
    /// Gets the full name of the aspect.
    /// </summary>
    public string FullName => $"{Namespace}.{Name}";

    /// <summary>
    /// The display name for the aspect.
    /// </summary>
    public string DisplayName { get; set; } = default!;
    
    /// <summary>
    /// The description of the aspect.
    /// </summary>
    public string Description { get; set; } = "";
}