using ZyphCare.Aspects.Contracts;
using ZyphCare.HealthRecords.Aspects;

namespace ZyphCare.HealthRecords.Extensions;

/// <summary>
/// Provides extension methods for the service collection to integrate the ZyphCare HealthRecords functionality.
/// </summary>
public static class UnitExtensions
{
    /// <summary>
    /// Adds the ZyphCare HealthRecords aspect to the current unit, enabling health records functionality.
    /// </summary>
    /// <param name="unit">The current unit to which the HealthRecords aspect is added.</param>
    /// <param name="config">An optional configuration action to customize the HealthRecords aspect.</param>
    /// <returns>Returns the updated unit with the HealthRecords aspect applied.</returns>
    public static IUnit AddZyphCareHealthRecords(this IUnit unit, Action<HealthRecordAspect>? config = default)
    {
        unit.Configure(config);
        return unit;
    }
}