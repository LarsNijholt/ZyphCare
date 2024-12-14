namespace ZyphCare.Aspects.Attributes;

/// <summary>
/// Specifies a dependency between a target class and another class.
/// </summary>
/// <remarks>
/// This attribute is used to denote that the target class depends on another class,
/// which is represented by the specified type. It is primarily used for configuring
/// or facilitating dependency injection, composition, or initialization processes
/// in software components or frameworks.
/// </remarks>
/// <example>
/// This attribute can be applied to a class multiple times to declare dependencies
/// on multiple types. Each instance specifies a single dependency.
/// </example>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependsOnAttribute : Attribute
{
    /// <inheritdoc />
    public DependsOnAttribute(Type type)
    {
        Type = type;
    }

    /// <summary>
    /// Represents a custom attribute used to declare a dependency on a specific type.
    /// </summary>
    /// <remarks>
    /// This attribute is applied to a class to indicate that it has a dependency
    /// on another class or type. It aids frameworks or systems in understanding
    /// the relationships and dependencies between components for operations like
    /// dependency injection or initialization scheduling.
    /// </remarks>
    public Type Type { get; }
}