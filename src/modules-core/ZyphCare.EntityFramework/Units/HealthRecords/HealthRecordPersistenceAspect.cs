using System.Data;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Aspects.Contracts;
using ZyphCare.EntityFramework.Common;
using ZyphCare.EntityFramework.Common.Contracts;
using ZyphCare.EntityFramework.Handlers;
using ZyphCare.EntityFramework.Units.Users;
using ZyphCare.HealthRecords.Entities;
using ZyphCare.Users.Aspects;
using ZyphCare.Users.Contracts;

namespace ZyphCare.EntityFramework.Units.HealthRecords;

public class HealthRecordPersistenceAspect(IUnit unit) : PersistenceAspectBase<HealthRecordZyphCareDbContext>(unit)
{
    public Func<IServiceProvider, IDbExceptionHandler<HealthRecordZyphCareDbContext>> DbExceptionHandler { get; set; } = _ => new RethrowDbExceptionHandler();

    public override void Configure()
    {
        Unit.Configure<UserAspect>(aspect =>
        {
            aspect.UserEntityStore = new Func<IServiceProvider, EfCoreUserStore>(sp => ServiceProviderServiceExtensions.GetRequiredService<EfCoreUserStore>(sp));
        });
    }

    public override void Apply()
    {
        base.Apply();
        Services.AddScoped(DbExceptionHandler);
        AddEntityStore<HealthRecord, EfCoreHealthRecordStore>();
    }
}