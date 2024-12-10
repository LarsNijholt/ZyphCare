using Elsa.Features.Abstractions;
using Elsa.Features.Services;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.EntityFramework.Common.Extensions;
using ZyphCare.Users.Contracts;
using ZyphCare.Users.Entities;
using ZyphCare.Users.Stores;

namespace ZyphCare.Users.Features;

/// <summary>
/// Installs user feature.
/// </summary>
public class UserFeature : FeatureBase
{
    /// <summary>
    /// A factory that instantiates a <see cref="IUserEntityStore"/>.
    /// </summary>
    public Func<IServiceProvider, IUserEntityStore> UserEntityStore { get; set; } =
        sp => sp.GetRequiredService<MemoryUserEntityStore>();

    /// <inheritdoc />
    public UserFeature(IModule module) : base(module)
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