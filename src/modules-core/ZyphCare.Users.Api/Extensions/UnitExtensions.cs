using ZyphCare.Aspects.Contracts;
using ZyphCare.Users.Api.Aspects;

namespace ZyphCare.Users.Api.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="IUnit"/> interface to enhance its functionality.
/// </summary>
public static class UnitExtensions
{
    /// <summary>
    /// Adds and configures the ZyphCare API aspect to the specified unit, allowing for enhanced API functionality.
    /// </summary>
    /// <param name="unit">The unit to which the ZyphCare API aspect will be applied.</param>
    /// <param name="configure">
    /// An optional configuration action to customize the behavior of the ZyphCare API aspect.
    /// If no action is provided, default configurations will be applied.
    /// </param>
    /// <returns>The configured <see cref="IUnit"/> instance, which includes the ZyphCare API aspect.</returns>
    public static IUnit UseZyphCareUsersApi(this IUnit unit, Action<ZyphCareApiAspect>? configure = default)
    {
        unit.Configure(configure);
        return unit;
    }
}