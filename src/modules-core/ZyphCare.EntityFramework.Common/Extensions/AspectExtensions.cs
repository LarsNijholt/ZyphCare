using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Aspects.Contracts;
using ZyphCare.Aspects.Extensions;
using ZyphCare.EntityFramework.Common.Features;

namespace ZyphCare.EntityFramework.Common.Extensions;

public static class AspectExtensions
{
    private static readonly IDictionary<IServiceCollection, IUnit> Units =
        new ConcurrentDictionary<IServiceCollection, IUnit>();

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