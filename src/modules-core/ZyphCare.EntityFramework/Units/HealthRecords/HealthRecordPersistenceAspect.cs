using System.Data;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Aspects.Contracts;
using ZyphCare.EntityFramework.Common;
using ZyphCare.EntityFramework.Common.Contracts;
using ZyphCare.EntityFramework.Handlers;
using ZyphCare.EntityFramework.Units.Users;
using ZyphCare.HealthRecords.Aspects;
using ZyphCare.HealthRecords.Entities;
using ZyphCare.Users.Aspects;
using ZyphCare.Users.Contracts;

namespace ZyphCare.EntityFramework.Units.HealthRecords;

public class HealthRecordPersistenceAspect(IUnit unit) : PersistenceAspectBase<HealthRecordZyphCareDbContext>(unit)
{
    public Func<IServiceProvider, IDbExceptionHandler<HealthRecordZyphCareDbContext>> DbExceptionHandler { get; set; } = _ => new RethrowDbExceptionHandler();

    /// <inheritdoc />
    public override void Configure()
    {
        Unit.Configure<HealthRecordAspect>(aspect =>
        {
            aspect.HealthRecordEntityStore = sp => sp.GetRequiredService<EfCoreHealthRecordStore>();
        });
    }

    /// <inheritdoc />
    public override void Apply()
    {
        base.Apply();
        Services.AddScoped(DbExceptionHandler);
        AddEntityStore<HealthRecord, EfCoreHealthRecordStore>();
    }
}