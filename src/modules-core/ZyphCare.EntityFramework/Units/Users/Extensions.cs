using ZyphCare.Users.Aspects;

namespace ZyphCare.EntityFramework.Units.Users;

/// <summary>
/// Extensions class provides extension methods for the <see cref="UserAspect"/>.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Configures the Entity Framework Core persistence for the <see cref="UserAspect"/>.
    /// </summary>
    public static UserAspect UseEntityFrameworkCore(this UserAspect aspect, Action<EfCoreUserPersistenceAspect>? configure = default)
    {
        aspect.Unit.Configure(configure);
        return aspect;
    }
}