using Microsoft.Extensions.DependencyInjection;

namespace ZyphCare.Aspects.Services;

/// <summary>
/// abstraction on top of the <see cref="IServiceCollection"/> to help organize units.
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
    
    bool HasUnit<T>() where T : IUnit;
}