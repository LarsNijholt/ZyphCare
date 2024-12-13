using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Aspects.Abstractions;
using ZyphCare.Aspects.Contracts;
using ZyphCare.EntityFramework.Common.Extensions;
using ZyphCare.Users.Contracts;
using ZyphCare.Users.Entities;
using ZyphCare.Users.Stores;

namespace ZyphCare.Users.Aspects;

/// <summary>
/// Installs user feature.
/// </summary>
public class UserAspect : BaseAspect
{
    /// <summary>
    /// A factory that instantiates a <see cref="IUserEntityStore"/>.
    /// </summary>
    public Func<IServiceProvider, IUserEntityStore> UserEntityStore { get; set; } =
        sp => sp.GetRequiredService<MemoryUserEntityStore>();

    /// <inheritdoc />
    public UserAspect(IUnit module) : base(module)
    {
    }

    /// <inheritdoc />
    public override void Apply()
    {
        Services
            .AddScoped(UserEntityStore)
            .AddMemoryStore<User, MemoryUserEntityStore>();
    }
}