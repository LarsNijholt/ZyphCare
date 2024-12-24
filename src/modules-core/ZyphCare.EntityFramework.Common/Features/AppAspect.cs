using ZyphCare.Aspects.Abstractions;
using ZyphCare.Aspects.Contracts;

namespace ZyphCare.EntityFramework.Common.Features;

/// <summary>
/// Represents an application-specific aspect by extending the base functionality
/// of <see cref="BaseAspect"/>. This class allows the addition of custom behaviors
/// or configurations within the context of an application.
/// </summary>
public class AppAspect : BaseAspect
{
    /// <inheritdoc />
    public AppAspect(IUnit unit) : base(unit)
    {
    }

    /// <summary>
    ///  The configurator to invoke.
    /// </summary>
    public Action<IUnit>? Configurator { get; set; }

    /// <inheritdoc />
    public override void Configure()
    {
        Configurator?.Invoke(Unit);
    }
}