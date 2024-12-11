using System.Collections.Concurrent;
using Elsa.Features.Implementations;
using Elsa.Features.Services;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.EntityFramework.Common.Features;

namespace ZyphCare.Extensions;

public static class DependencyInjectionExtensions
{
    private static readonly IDictionary<IServiceCollection, IModule> Units = new ConcurrentDictionary<IServiceCollection, IModule>();

    /// <summary>
    /// Adds and configures ZyphCare units to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the ZyphCare units will be added.</param>
    /// <param name="configure">An optional action to configure the IModule instance.</param>
    /// <returns>The configured IModule instance representing the ZyphCare unit.</returns>
    public static IModule AddZyphCareUnits(this IServiceCollection services, Action<IModule>? configure = default)
    {
        var unit = services.GetOrCreateUnit();
        unit.Configure<AppFeature>(app => app.Configurator = configure);
        unit.Apply();

        return unit;
    }

    private static IModule GetOrCreateUnit(this IServiceCollection services)
    {
        if (Units.TryGetValue(services, out var unit))
            return unit;

        unit = services.CreateUnit();

        Units[services] = unit;

        return unit;
    }

    public static IModule CreateUnit(this IServiceCollection services) => new Module(services);
    
    public static IModule Use<T>(this IModule module, Action<T>? configure = default) where T: class, IFeature
    {
        module.Configure(configure);
        return module;
    }
}