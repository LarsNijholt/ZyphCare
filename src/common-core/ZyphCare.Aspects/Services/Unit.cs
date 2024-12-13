using System.ComponentModel;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Aspects.Attributes;
using ZyphCare.Aspects.Contracts;
using ZyphCare.Aspects.Extensions;
using ZyphCare.Aspects.Models;

namespace ZyphCare.Aspects.Services;

/// <inheritdoc />
public class Unit : IUnit
{
    private Dictionary<Type, IAspect> _aspects = new();
    private readonly HashSet<IAspect> _configuredAspects = new();
    private bool _isApplying;
    
    /// <summary>
    /// Represents a concrete implementation of the <see cref="IUnit"/> interface,
    /// providing access to a collection of services and properties, as well as
    /// methods for configuring and applying aspects.
    /// </summary>
    public Unit(IServiceCollection services)
    {
        Services = services;
    }

    /// <inheritdoc />
    public IServiceCollection Services { get; }
    /// <inheritdoc />
    public IDictionary<object, object> Properties { get; } = new Dictionary<object, object>();

    /// <inheritdoc />
    public bool HasAspect<T>() where T : IUnit
    {
        return HasAspect(typeof(T));
    }

    /// <inheritdoc />
    public bool HasAspect(Type aspectType)
    {
        return _aspects.ContainsKey(aspectType);
    }

    /// <inheritdoc />
    public T Configure<T>(Action<T>? configure = default) where T : class, IAspect
        => Configure(unit => (T)Activator.CreateInstance(typeof(T), unit)!, configure);

    /// <inheritdoc />
    public T Configure<T>(Func<IUnit, T> factory, Action<T>? configure = default) where T : class, IAspect
    {
        if (!_aspects.TryGetValue(typeof(T), out var aspect))
        {
            aspect = factory(this);
            _aspects[typeof(T)] = aspect;
        }
        
        configure?.Invoke((T)aspect);
        if(!_isApplying)
            return (T)aspect;

        var dependencies = GetDependencyTypes(aspect.GetType()).ToHashSet();
        foreach (var dependency in dependencies.Select(GetOrCreateAspect))
        {
            ConfigureAspect(dependency);
        }
        
        ConfigureAspect(aspect);
        return (T)aspect;
    }

    /// <inheritdoc />
    public void Apply()
    {
        _isApplying = true;
        var aspectTypes = GetAspectTypes();
        _aspects = aspectTypes.ToDictionary(aspectType => aspectType, aspectType => _aspects.TryGetValue(aspectType, out var existingAspect) ? existingAspect : (IAspect)Activator.CreateInstance(aspectType, this)!);

        // Iterate over a copy of the aspects to avoid concurrent modification exceptions.
        foreach (var aspect in _aspects.Values.ToList())
        {
            // This will cause additional aspects to be added to _aspects.
            ConfigureAspect(aspect);
        }
        
        // Filter out aspects that depend on other aspects that are not installed.
        _aspects = ExcludeAspectsWithMissingDependencies(_aspects.Values).ToDictionary(x => x.GetType(), x => x);
        
        // Make sure to use the complete list of aspects when applying them.
        foreach (var aspect in _aspects.Values)
        {
            aspect.Apply();
        }

        var registry = new InstalledAspectRegistry();
        foreach (var aspects in _aspects.Values)
        {
            var type = aspects.GetType();
            var name = type.Name.Replace("Aspect", string.Empty);
            var ns = "ZyphCare";
            var displayName = type.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? name;
            var description = type.GetCustomAttribute<DescriptionAttribute>()?.Description;
            registry.Add(new AspectDescriptor(name, ns, displayName, description));
        }
        
        Services.AddSingleton<IInstalledAspectRegistry>(registry);
    }

    private IEnumerable<IAspect> ExcludeAspectsWithMissingDependencies(IEnumerable<IAspect> aspects)
    {
        return aspects
            .Where(feature =>
            {
                var featureType = feature.GetType();
                var dependencyOfAttributes = featureType.GetCustomAttributes<DependencyOfAttribute>().ToList();
                var missingDependencies = dependencyOfAttributes.Where(x => !_aspects.ContainsKey(x.Type)).ToList();
                return missingDependencies.Count == 0;
            });
    }

    private void ConfigureAspect(IAspect aspect)
    {
        if(_configuredAspects.Contains(aspect))
            return;
        
        aspect.Configure();
        _aspects[aspect.GetType()] = aspect;
        _configuredAspects.Add(aspect);
    }

    private IAspect GetOrCreateAspect(Type aspectType)
    {
        return _aspects.TryGetValue(aspectType, out var existingAspect) ? existingAspect : (IAspect)Activator.CreateInstance(aspectType, this)!;
    }

    private HashSet<Type> GetAspectTypes()
    {
        var aspectTypes = _aspects.Keys.ToHashSet();
        var aspectTypesWithDependencies = aspectTypes.Concat(aspectTypes.SelectMany(GetDependencyTypes)).ToHashSet();
        return aspectTypesWithDependencies.TSort(x => x.GetCustomAttributes<DependsOnAttribute>().Select(dependsOn => dependsOn.Type)).ToHashSet();
    }

    // Recursively get dependency types.
    private IEnumerable<Type> GetDependencyTypes(Type type)
    {
        var dependencies = type.GetCustomAttributes<DependsOnAttribute>().Select(dependsOn => dependsOn.Type).ToList();
        return dependencies.Concat(dependencies.SelectMany(GetDependencyTypes));
    }
}