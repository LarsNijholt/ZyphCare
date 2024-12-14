using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ZyphCare.Aspects.Contracts;

/// <summary>
/// Represents a unit abstraction, providing access to services and properties.
/// </summary>
public interface IUnit
{
    /// <summary>
    /// Gets the service collection used to register and resolve dependencies within the unit.
    /// </summary>
    IServiceCollection Services { get; }

    /// <summary>
    /// Gets the collection of properties that can be used to store and retrieve custom data associated with the unit.
    /// </summary>
    IDictionary<object, object> Properties { get; }

    /// <summary>
    /// Determines whether the specified unit of type T exists.
    /// </summary>
    /// <typeparam name="T">The type of the unit to check, which must implement the IUnit interface.</typeparam>
    /// <returns>True if the unit of type T exists; otherwise, false.</returns>
    bool HasAspect<T>() where T : IUnit;

    /// <summary>
    /// Determines whether the specified unit of the given type exists.
    /// </summary>
    /// <param name="aspectType">The type of the unit to check.</param>
    /// <returns>True if a unit of the specified type exists; otherwise, false.</returns>
    bool HasAspect(Type aspectType);


    /// <summary>
    /// Configures an instance of the specified aspect type by applying the given configuration action.
    /// </summary>
    /// <typeparam name="T">The type of the aspect to configure, which must implement the IAspect interface.</typeparam>
    /// <param name="configure">An optional action to apply additional configuration to the aspect instance.</param>
    /// <returns>The configured aspect instance of type T.</returns>
    T Configure<T>(Action<T>? configure = default) where T : class, IAspect;

    /// <summary>
    /// Configures an aspect of the specified type using the provided factory and optional configuration action.
    /// </summary>
    /// <typeparam name="T">The type of the aspect to configure, which must implement the IAspect interface.</typeparam>
    /// <param name="factory">A function that creates an instance of the aspect using the provided IUnit.</param>
    /// <param name="configure">An optional action to apply additional configuration to the created aspect instance.</param>
    /// <returns>The configured aspect instance of type T.</returns>
    T Configure<T>(Func<IUnit, T> factory, Action<T>? configure = default) where T : class, IAspect;

    /// <summary>
    /// Configures a <see cref="IHostedService"/> using an optional priority to control in which order it will be registered with the service container.
    /// </summary>
    IUnit ConfigureHostedService<T>(int priority = 0) where T : class, IHostedService;

    /// <summary>
    /// Configures a <see cref="IHostedService"/> using an optional priority to control in which order it will be registered with the service container.
    /// </summary>
    IUnit ConfigureHostedService(Type hostedServiceType, int priority = 0);
    
    /// <summary>
    /// Applies all configured aspects, causing the <see cref="Services"/> collection to be populated.
    /// </summary>
    void Apply();
}