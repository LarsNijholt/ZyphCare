using ZyphCare.Users.Features;

namespace ZyphCare.EntityFramework.Modules.Users;

/// <summary>
/// Extensions class provides extension methods for the <see cref="UserFeature"/>.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Configures the Entity Framework Core persistence for the <see cref="UserFeature"/>.
    /// </summary>
    public static UserFeature UseEntityFrameworkCore(this UserFeature feature, Action<EfCoreUserPersistenceFeature>? configure = default)
    {
        feature.Module.Configure(configure);
        return feature;
    }
}