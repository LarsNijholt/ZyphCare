using Elsa.Features.Services;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.EntityFramework.Common;
using ZyphCare.EntityFramework.Common.Contracts;
using ZyphCare.EntityFramework.Handlers;
using ZyphCare.Users.Contracts;
using ZyphCare.Users.Entities;
using ZyphCare.Users.Features;

namespace ZyphCare.EntityFramework.Modules.Users;

/// <summary>
/// Module for the user to use EF Core persistence providers.
/// </summary>
public class EfCoreUserPersistenceFeature(IModule module) : PersistenceFeatureBase<UserZyphCareDbContext>(module)
{
    /// <summary>
    /// Delegate for determining the exception handler.
    /// </summary>
    public Func<IServiceProvider, IDbExceptionHandler<UserZyphCareDbContext>> DbExceptionHandler { get; set; } = _ => new RethrowDbExceptionHandler();

    /// <inheritdoc />
    public override void Configure()
    {
        Module.Configure<UserFeature>(feature =>
        {
            feature.UserEntityStore = new Func<IServiceProvider, EfCoreUserStore>(sp => ServiceProviderServiceExtensions.GetRequiredService<EfCoreUserStore>(sp));
        });
    }

    /// <inheritdoc />
    public override void Apply()
    {
        base.Apply();
        Services.AddScoped(DbExceptionHandler);
        AddEntityStore<User, EfCoreUserStore>();
    }
}