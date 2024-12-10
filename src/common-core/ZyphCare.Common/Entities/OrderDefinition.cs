using System.Linq.Expressions;
using ZyphCare.EntityFramework.Common.Entities;

namespace ZyphCare.Common.Entities;

/// <summary>
/// Represents the order by which to order the results of a query.
/// </summary>
public class OrderDefinition<T, TProp>
{
    /// <summary>
    /// Creates a new instance of the <see cref="OrderDefinition{T, TProp}"/> class.
    /// </summary>
    public OrderDefinition()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="OrderDefinition{T, TProp}"/> class.
    /// </summary>
    public OrderDefinition(Expression<Func<T, TProp>> keySelector, OrderDirection orderDirection)
    {
        KeySelector = keySelector;
        Direction = orderDirection;
    }
    
    /// <summary>
    /// The direction in which to order the results.
    /// </summary>
    public OrderDirection Direction { get; set; }
    
    /// <summary>
    /// The key selector to use to order the results.
    /// </summary>
    public Expression<Func<T, TProp>> KeySelector { get; set; } = default!;
}