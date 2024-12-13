using ZyphCare.Aspects.Contracts;
using ZyphCare.Users.Features;

namespace ZyphCare.Users.Extensions;

public static class ServiceCollectionExtensions
{
    public static IUnit AddZyphCareUsers(this IUnit module, Action<UserFeature>? config)
    {
         module.Configure(config);
         return module;
    }
}