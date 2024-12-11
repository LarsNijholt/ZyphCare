using Elsa.Features.Services;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Users.Features;

namespace ZyphCare.Users.Extensions;

public static class ServiceCollectionExtensions
{
    public static IModule AddZyphCareUsers(this IModule module, Action<UserFeature>? config)
    {
         module.Configure(config);
         return module;
    }
}