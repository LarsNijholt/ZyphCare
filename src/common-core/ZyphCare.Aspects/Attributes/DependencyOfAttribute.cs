namespace ZyphCare.Aspects.Attributes;

/// <summary>
/// Specifies that the aspect is enabled automatically when the specified feature is enabled.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependencyOfAttribute : Attribute
{
    /// <inheritdoc />
    public DependencyOfAttribute(Type type)
    {
        Type = type;
    }
    
    /// <summary>
    /// The type of the feature this aspect is a dependency of.
    /// </summary>
    public Type Type { get; set; }
}