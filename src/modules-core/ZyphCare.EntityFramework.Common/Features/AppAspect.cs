using ZyphCare.Aspects.Abstractions;
using ZyphCare.Aspects.Contracts;

namespace ZyphCare.EntityFramework.Common.Features;

public class AppAspect : BaseAspect
{
    public AppAspect(IUnit unit) : base(unit)
    {
    }

    /// <summary>
///     The configurator to invoke.
    /// </summary>
    public Action<IUnit>? Configurator { get; set; }

    /// <inheritdoc />
    public override void Configure()
    {
        Configurator?.Invoke(Unit);
    }
}