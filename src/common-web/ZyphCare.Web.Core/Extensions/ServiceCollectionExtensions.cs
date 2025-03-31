using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ZyphCare.Api.Client.Extensions;
using ZyphCare.Web.Core.Contracts;
using ZyphCare.Web.Core.Models;
using ZyphCare.Web.Core.Services;

namespace ZyphCare.Web.Core.Extensions;

/// <summary>
/// Provides extension methods for adding services related to the remote backend configuration and access to the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds core services and configurations, including dependencies required for managing and initializing aspects.
    /// </summary>
    /// <param name="services">The service collection to which the core services and configurations are added.</param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> with the core services and configurations added.
    /// </returns>
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services
            .AddScoped<IMenuService, DefaultMenuService>()
            .AddScoped<IAspectService, DefaultAspectService>();
    }
    
    /// <summary>
    /// Adds services and configurations necessary for interacting with a remote backend API to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which the remote backend services and configurations are added.</param>
    /// <param name="config">
    /// An optional <see cref="BackendApiConfig"/> instance that provides configuration for the backend API,
    /// including HTTP client builder settings and backend-specific options.
    /// If not provided, default configurations will be used.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> with the remote backend services and configurations added.
    /// </returns>
    public static IServiceCollection AddRemoteBackend(this IServiceCollection services, BackendApiConfig? config = null)
    {
        services.Configure(config?.ConfigureBackendOptions ?? (_ => { }));
        services.AddDefaultApiClients(config?.ConfigureHttpClientBuilder);
        services.TryAddScoped<IRemoteBackendAccessor, DefaultRemoteBackendAccessor>();
        services.TryAddScoped<IBlazorServiceAccessor, BlazorServiceAccessor>();
        services.TryAddScoped<IBackendApiClientProvider, DefaultBackendApiClientProvider>();
        return services;
    }
}