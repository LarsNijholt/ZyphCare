using ZyphCare.Api.Common.Extensions;
using ZyphCare.Aspects.Abstractions;
using ZyphCare.Aspects.Contracts;

namespace ZyphCare.Users.Api.Aspects;

/// <summary>
/// Provides ZyphCare-specific API behavior by extending the functionality of the BaseAspect class.
/// Acts as a component that ensures reusable services and configurations can be applied within the ZyphCare.Users.Api context.
/// </summary>
public class ZyphCareApiAspect : BaseAspect
{
    /// <inheritdoc />
    public ZyphCareApiAspect(IUnit unit) : base(unit)
    {
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Unit.AddFastEndpointsAssembly(GetType());
    }
}