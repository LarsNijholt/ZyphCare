using ZyphCare.Aspects.Abstractions;
using ZyphCare.Aspects.Contracts;

namespace ZyphCare.Common.Aspects;

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