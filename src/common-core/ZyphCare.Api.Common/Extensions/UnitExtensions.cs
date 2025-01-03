using System.Reflection;
using FastEndpoints;
using ZyphCare.Aspects.Contracts;

namespace ZyphCare.Api.Common.Extensions;

/// <summary>
/// Provides extension methods for the IUnit interface, enabling configuration and management
/// of FastEndpoints assemblies and related functionalities for a modular application.
/// </summary>
public static class UnitExtensions
{
    private static readonly object FastEndpointsAssembliesKey = new();

    /// <summary>
    /// Registers the specified assembly for FastEndpoint assembly discovery.
    /// </summary>
    public static IUnit AddFastEndpointsAssembly(this IUnit unit, Assembly assembly)
    {
        var assemblies = unit.Properties.GetOrAdd(FastEndpointsAssembliesKey, () => new HashSet<Assembly>());
        assemblies.Add(assembly);
        return unit;
    }
    
    /// <summary>
    /// Registers the assembly for FastEndpoint assembly discovery using the specified marker type.
    /// </summary>
    public static IUnit AddFastEndpointsAssembly<T>(this IUnit module) => module.AddFastEndpointsAssembly(typeof(T));

    /// <summary>
    /// Registers the assembly for FastEndpoint assembly discovery using the specified marker type.
    /// </summary>
    public static IUnit AddFastEndpointsAssembly(this IUnit module, Type markerType) => module.AddFastEndpointsAssembly(markerType.Assembly);

    /// <summary>
    /// Returns all collected assemblies for discovery of endpoints.
    /// </summary>
    public static IEnumerable<Assembly> GetFastEndpointsAssembliesFromModule(this IUnit module) => module.Properties.GetOrAdd(FastEndpointsAssembliesKey, () => new HashSet<Assembly>());

    /// <summary>
    /// Adds FastEndpoints to the service container and registers all collected assemblies for discovery of endpoints.
    /// </summary>
    public static IUnit AddFastEndpointsFromModule(this IUnit module)
    {
        var assemblies = module.GetFastEndpointsAssembliesFromModule().ToList();

        module.Services.AddFastEndpoints(options =>
        {
            options.DisableAutoDiscovery = true;
            options.Assemblies = assemblies;
        });

        return module;
    }
}