using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Aspects.Contracts;
using ZyphCare.Aspects.Services;

namespace ZyphCare.Aspects.Extensions;

/// <summary>
/// Adds extension methods to <see cref="IServiceCollection"/> that creates and configures units.  
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Returns a new instance of an <see cref="IUnit"/> implementation.
    /// </summary>
    public static IUnit CreateUnit(this IServiceCollection services) => new Unit(services);

    /// <summary>
    /// Installs and configures the specified aspect. If the aspect was already installed, it is not added twice, which means it is safe to call this method multiple times.
    /// </summary>
    public static IUnit Use<T>(this IUnit unit, Action<T>? configure = default) where T: class, IAspect
    {
        unit.Configure(configure);
        return unit;
    }
}