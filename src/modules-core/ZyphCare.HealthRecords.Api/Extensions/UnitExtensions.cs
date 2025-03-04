using ZyphCare.Aspects.Contracts;
using ZyphCare.HealthRecords.Api.Aspect;

namespace ZyphCare.HealthRecords.Api.Extensions;

/// <summary>
/// Provides extension methods for enhancing the functionality of the <see cref="IUnit"/> interface,
/// specifically for integrating ZyphCare HealthRecords API aspects.
/// </summary>
public static class UnitExtensions
{
    /// <summary>
    /// Adds the ZyphCare HealthRecords API aspect to the specified unit, enabling
    /// API-related behavior and configuration for ZyphCare HealthRecords.
    /// </summary>
    /// <param name="unit">The unit to which the ZyphCare HealthRecords API aspect is added.</param>
    /// <param name="config">An optional configuration action to further customize the behavior of the ZyphCareApiAspect.</param>
    /// <returns>The updated unit with the ZyphCare HealthRecords API aspect configured.</returns>
    public static IUnit AddZyphCareHealthRecordsApi(this IUnit unit, Action<ZyphCareApiAspect>? config = default)
    {
        unit.Configure(config);
        return unit;
    }
}