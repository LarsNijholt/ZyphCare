using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Aspects.Contracts;
using ZyphCare.Aspects.Extensions;
using ZyphCare.EntityFramework.Common.Features;

namespace ZyphCare.EntityFramework.Common.Extensions;

/// <summary>
/// Provides extension methods for configuring and managing ZyphCare Entity Framework modules within the DI container.
/// </summary>
public static class AspectExtensions
{
    private static readonly IDictionary<IServiceCollection, IUnit> Units =
        new ConcurrentDictionary<IServiceCollection, IUnit>();

    /// <summary>
    /// Configures the ZyphCare Entity Framework module within the service collection and applies additional configurations as needed.
    /// </summary>
    /// <param name="serviceCollection">The service collection to which the ZyphCare Entity Framework module is added.</param>
    /// <param name="configure">An optional action to configure the unit module further.</param>
    /// <returns>The configured unit module that allows additional extension and configuration.</returns>
    public static IUnit AddZyphCareEntityFramework(this IServiceCollection serviceCollection,
        Action<IUnit>? configure = default)
    {
        var unit = serviceCollection.GetOrCreateModule();
        unit.Configure<AppAspect>(app => app.Configurator = configure);
        unit.Apply();
        return unit;
    }

    private static IUnit GetOrCreateModule(this IServiceCollection services)
    {
        if (Units.TryGetValue(services, out var unit))
            return unit;

        unit = services.CreateUnit();

        Units[services] = unit;
        return unit;
    }
}