using ZyphCare.HealthRecords.Aspects;

namespace ZyphCare.EntityFramework.Units.HealthRecords;

public static class Extensions
{
    public static HealthRecordAspect UseEntityFrameworkCore(this HealthRecordAspect aspect, Action<HealthRecordPersistenceAspect>? configure = default)
    {
        aspect.Unit.Configure(configure);
        return aspect;
    }
}