using ZyphCare.Aspects.Contracts;
using ZyphCare.Users.Aspects;

namespace ZyphCare.Users.Extensions;

public static class ServiceCollectionExtensions
{
    public static IUnit AddZyphCareUsers(this IUnit module, Action<UserAspect>? config)
    {
         module.Configure(config);
         return module;
    }
}