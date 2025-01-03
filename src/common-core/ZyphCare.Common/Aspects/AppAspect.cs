using ZyphCare.Aspects.Abstractions;
using ZyphCare.Aspects.Contracts;

namespace ZyphCare.Common.Aspects;

/// <summary>
/// Represents an application-specific aspect implemented by extending the base aspect functionality.
/// This class serves as a customizable component where additional configuration logic
/// can be applied to the provided <see cref="IUnit"/> via the <see cref="Configurator"/> property.
/// </summary>
public class AppAspect : BaseAspect
{
    /// <inheritdoc />
    public AppAspect(IUnit module) : base(module)
    {
    }
    
    /// <summary>
    /// The configurator to invoke.
    /// </summary>
    public Action<IUnit>? Configurator { get; set; }

    /// <inheritdoc />
    public override void Configure()
    {
        Configurator?.Invoke(Unit);
    }
}