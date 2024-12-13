using ZyphCare.Aspects.Models;

namespace ZyphCare.Aspects.Contracts;
/// <summary>
/// Represents a registry of installed aspects.
/// </summary>
public interface IInstalledAspectRegistry
{
    /// <summary>
    /// Adds a aspect descriptor to the registry.
    /// </summary>
    /// <param name="descriptor">The aspect descriptor.</param>
    void Add(AspectDescriptor descriptor);
    
    /// <summary>
    /// Gets all installed aspects.
    /// </summary>
    /// <returns>All installed aspects.</returns>
    IEnumerable<AspectDescriptor> List();
    
    /// <summary>
    /// Finds a aspect descriptor by its full name.
    /// </summary>
    /// <param name="fullName">The full name of the aspect.</param>
    /// <returns>The aspect descriptor or null if not found.</returns>
    AspectDescriptor? Find(string fullName);
}