using Microsoft.Extensions.DependencyInjection;
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
    public void Configure()
    {
    }
    
    /// <summary>
    /// Override this to register services with <see cref="Services"/>.
    /// </summary>
    public void Apply()
    {
    }
}