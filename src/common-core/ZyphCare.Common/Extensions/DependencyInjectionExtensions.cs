using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Aspects.Contracts;
using ZyphCare.Aspects.Services;
using ZyphCare.Common.Aspects;

namespace ZyphCare.Common.Extensions;

public static class DependencyInjectionExtensions
{
    private static readonly IDictionary<IServiceCollection, IUnit> Units = new ConcurrentDictionary<IServiceCollection, IUnit>();

    /// <summary>
    /// Adds and configures ZyphCare units to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the ZyphCare units will be added.</param>
    /// <param name="configure">An optional action to configure the IModule instance.</param>
    /// <returns>The configured IModule instance representing the ZyphCare unit.</returns>
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

    public static IUnit CreateUnit(this IServiceCollection services) => new Unit(services);
    
    public static IUnit Use<T>(this IUnit module, Action<T>? configure = default) where T: class, IAspect
    {
        module.Configure(configure);
        return module;
    }
}