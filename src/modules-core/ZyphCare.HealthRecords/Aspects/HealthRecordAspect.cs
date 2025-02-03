using Microsoft.Extensions.DependencyInjection;
using ZyphCare.Aspects.Abstractions;
using ZyphCare.Aspects.Contracts;
using ZyphCare.EntityFramework.Common.Extensions;
using ZyphCare.HealthRecords.Contracts;
using ZyphCare.HealthRecords.Entities;
using ZyphCare.HealthRecords.Store;

namespace ZyphCare.HealthRecords.Aspects;

/// <summary>
/// Installs HealthRecord aspect.
/// </summary>
public class HealthRecordAspect : BaseAspect
{
    /// <summary>
    /// A factory that instantiates a <see cref="IHealthRecordEntityStore"/>.
    /// </summary>
    public Func<IServiceProvider, IHealthRecordEntityStore> HealthRecordEntityStore { get; set; } = 
        sp => sp.GetRequiredService<MemoryHealthRecordEntityStore>();
    
    /// <inheritdoc />
    public HealthRecordAspect(IUnit unit) : base(unit)
    {
    }

    /// <inheritdoc />
    public override void Apply()
    {
        Services
            .AddScoped(HealthRecordEntityStore)
            .AddMemoryStore<HealthRecord, MemoryHealthRecordEntityStore>();
    }
}