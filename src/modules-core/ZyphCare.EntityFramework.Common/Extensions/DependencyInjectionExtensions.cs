using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ZyphCare.EntityFramework.Common.Services;

namespace ZyphCare.EntityFramework.Common.Extensions;

/// <summary>
/// Provides extension methods for dependency injection.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds a memory-backed store for the specified entity type and store type to the service collection.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the memory store will manage.</typeparam>
    /// <typeparam name="TStore">The type of the store implementation to be registered.</typeparam>
    /// <param name="services">The service collection to which the memory store and store type are added.</param>
    /// <returns>The updated service collection with the memory store and store type registered.</returns>
    public static IServiceCollection AddMemoryStore<TEntity, TStore>(this IServiceCollection services)
        where TStore : class
    {
        services.TryAddSingleton<MemoryStore<TEntity>>();
        services.TryAddScoped<TStore>();
        return services;
    }
}