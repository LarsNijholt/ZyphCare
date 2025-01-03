using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Aspects.Contracts;
using ZyphCare.Aspects.Services;
using ZyphCare.Common.Aspects;

namespace ZyphCare.Common.Extensions;

/// <summary>
/// Provides extension methods for integrating ZyphCare units
/// and aspects into the dependency injection system.
/// </summary>
public static class DependencyInjectionExtensions
{
    private static readonly IDictionary<IServiceCollection, IUnit> Units = new ConcurrentDictionary<IServiceCollection, IUnit>();

    /// <summary>
    /// Adds and configures ZyphCare units to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the ZyphCare units will be added.</param>
    /// <param name="configure">An optional action to configure the IUnit instance.</param>
    /// <returns>The configured IUnit instance representing the ZyphCare unit.</returns>
    public static IUnit AddZyphCareUnits(this IServiceCollection services, Action<IUnit>? configure = default)
    {
        var unit = services.GetOrCreateUnit();
        unit.Configure<AppAspect>(app => app.Configurator = configure);
        unit.Apply();

        return unit;
    }

    private static IUnit GetOrCreateUnit(this IServiceCollection services)
    {
        if (Units.TryGetValue(services, out var unit))
            return unit;

        unit = services.CreateUnit();

        Units[services] = unit;

        return unit;
    }

    /// <summary>
    /// Creates and initializes a new instance of the Unit class associated with the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection with which the new Unit instance will be associated.</param>
    /// <returns>A new IUnit instance that encapsulates the provided IServiceCollection.</returns>
    public static IUnit CreateUnit(this IServiceCollection services) => new Unit(services);

    /// <summary>
    /// Configures and applies a specified aspect to the given IUnit instance.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the aspect to configure and apply, which must implement the IAspect interface.
    /// </typeparam>
    /// <param name="module">The IUnit instance to which the aspect will be applied.</param>
    /// <param name="configure">An optional action to configure the aspect of the specified type.</param>
    /// <returns>The IUnit instance with the configured aspect applied.</returns>
    public static IUnit Use<T>(this IUnit module, Action<T>? configure = default) where T: class, IAspect
    {
        module.Configure(configure);
        return module;
    }
}