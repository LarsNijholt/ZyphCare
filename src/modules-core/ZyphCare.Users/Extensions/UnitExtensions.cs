using ZyphCare.Aspects.Contracts;
using ZyphCare.Users.Aspects;

namespace ZyphCare.Users.Extensions;

/// <summary>
/// Provides extension methods for configuring ZyphCare user-related services within a module.
/// </summary>
public static class UnitExtensions
{
    /// <summary>
    /// Adds ZyphCare user services and configurations to the specified unit.
    /// </summary>
    /// <param name="unit">The current unit where the user services will be added.</param>
    /// <param name="config">An optional configuration action for customizing the <see cref="UserAspect"/> behavior.</param>
    /// <returns>The modified unit with ZyphCare user services added.</returns>
    public static IUnit AddZyphCareUsers(this IUnit unit, Action<UserAspect>? config)
    {
        unit.Configure(config);
        return unit;
    }
}