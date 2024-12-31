using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Web.Core.Contracts;
using ZyphCare.Web.Identity.Contracts;
using ZyphCare.Web.Identity.Services;

namespace ZyphCare.Web.Identity.Extensions;

/// <summary>
/// Provides extension methods to enhance the functionality of the IServiceCollection
/// for registering identity-related services within the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers identity-related services, including authentication providers, into the service collection.
    /// </summary>
    /// <param name="services">The service collection to which the identity services will be added.</param>
    /// <returns>The updated service collection with identity services registered.</returns>
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        return services
            .AddBlazoredLocalStorage()
            .AddAuthorizationCore()
            .AddHttpContextAccessor()
            .AddScoped<IAspect, Aspect>()
            .AddScoped<IAuthenticationProvider, AuthenticationProvider>();
    }
}