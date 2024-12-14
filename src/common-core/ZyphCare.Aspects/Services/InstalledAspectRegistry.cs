using ZyphCare.Aspects.Contracts;
using ZyphCare.Aspects.Models;

namespace ZyphCare.Aspects.Services;

/// <inheritdoc />
public class InstalledAspectRegistry : IInstalledAspectRegistry
{
    private readonly Dictionary<string, AspectDescriptor> _descriptors = new();

    /// <inheritdoc />
    public void Add(AspectDescriptor descriptor) => _descriptors[descriptor.FullName] = descriptor;

    /// <inheritdoc />
    public IEnumerable<AspectDescriptor> List() => _descriptors.Values;

    /// <inheritdoc />
    public AspectDescriptor? Find(string fullName) => _descriptors.GetValueOrDefault(fullName);
}