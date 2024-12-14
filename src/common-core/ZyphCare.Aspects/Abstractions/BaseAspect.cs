using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZyphCare.Aspects.Contracts;

namespace ZyphCare.Aspects.Abstractions;

/// <summary>
/// Base type for classes that represent an aspect.
/// </summary>
public abstract class BaseAspect : IAspect
{
    /// <summary>
    /// Constructor.
    /// </summary>
    protected BaseAspect(IUnit unit)
    {
        Unit = unit;
    }
    
    /// <summary>
    /// The unit this aspect is a part of.
    /// </summary>
    public IUnit Unit { get; }
    
    /// <summary>
    /// A reference to the <see cref="IServiceCollection"/> to which services can be added.
    /// </summary>
    public IServiceCollection Services => Unit.Services;
    
    /// <summary>
    /// Override this method to configure the aspect.
    /// </summary>
    public virtual void Configure()
    {
    }

    /// <inheritdoc />
    public virtual void ConfigureHostedServices()
    {
    }

    /// <summary>
    /// Override this to register services with <see cref="Services"/>.
    /// </summary>
    public virtual void Apply()
    {
    }
    
    /// <summary>
    /// Configures the specified hosted service using an optional priority to control in which order it will be registered with the service container.
    /// </summary>
    /// <param name="priority">The priority.</param>
    /// <typeparam name="T">The type of hosted service to configure.</typeparam>
    protected void ConfigureHostedService<T>(int priority = 0) where T : class, IHostedService
    {
        Unit.ConfigureHostedService<T>(priority);
    }
}