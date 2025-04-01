using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Web.Core.Contracts;
using ZyphCare.Web.HealthRecords.Menu;

namespace ZyphCare.Web.HealthRecords.Extensions;

/// <summary>
/// Provides extension methods for IServiceCollection to register HealthRecords services and dependencies.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds HealthRecords services and dependencies to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the services are added.</param>
    /// <returns>The IServiceCollection with the HealthRecords services registered.</returns>
    public static IServiceCollection AddHealthRecords(this IServiceCollection services)
    {
        return services
            .AddScoped<IAspect, Aspect>()
            .AddScoped<IMenuProvider, HealthRecordMenu>();
    }
}