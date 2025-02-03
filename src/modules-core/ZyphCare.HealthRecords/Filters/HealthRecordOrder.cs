using System.Linq.Expressions;
using ZyphCare.Common.Entities;
using ZyphCare.HealthRecords.Entities;

namespace ZyphCare.HealthRecords.Filters;

/// <summary>
/// Represents an order definition specific to the <see cref="HealthRecord"/> entity,
/// facilitating sorting of health records based on a specified property and direction.
/// </summary>
/// <typeparam name="TProp">The type of the property used for ordering.</typeparam>
public class HealthRecordOrder<TProp> : OrderDefinition<HealthRecord, TProp>
{
    /// <inheritdoc />
    public HealthRecordOrder()
    {
    }

    /// <inheritdoc />
    public HealthRecordOrder(Expression<Func<HealthRecord, TProp>> keySelector, OrderDirection orderDirection)
        : base(keySelector, orderDirection)
    {
        
    }
}