using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Users.Features;

namespace ZyphCare.Users.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddZyphCareUsers(this IServiceCollection services, Action<UserFeature>? config)
    {
        return services.Configure(config);
    }
}