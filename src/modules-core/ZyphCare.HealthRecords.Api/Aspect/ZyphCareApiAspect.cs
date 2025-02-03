using ZyphCare.Api.Common.Extensions;
using ZyphCare.Aspects.Abstractions;
using ZyphCare.Aspects.Contracts;

namespace ZyphCare.HealthRecords.Api.Aspect;

/// <summary>
/// Represents a specific implementation of the <see cref="BaseAspect"/> class
/// designed for the ZyphCare HealthRecords API. This aspect provides reusable
/// functionality or behavior tailored for the API layer.
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