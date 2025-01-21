using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor.Popups;
using ZyphCare.Studio.Dashboard.HttpMessageHandler;
using ZyphCare.Web.Core.Contracts;

namespace ZyphCare.Studio.Dashboard.Extensions;

/// <summary>
/// Provides extension methods for configuring services related to the ZyphCare Studio Dashboard.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the services required for the ZyphCare Studio Dashboard to the dependency injection container.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to which the Studio Dashboard services will be added.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> containing the registered services.
    /// </returns>
    public static IServiceCollection AddStudioDashboard(this IServiceCollection services)
    {
        return services
            .AddScoped<IAspect, Aspect>()
            .AddScoped<AuthenticatingApiHttpMessageHandler>();
    }
}