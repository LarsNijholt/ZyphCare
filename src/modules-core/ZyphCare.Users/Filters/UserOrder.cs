using System.Linq.Expressions;
using ZyphCare.Common.Entities;
using ZyphCare.EntityFramework.Common.Entities;
using ZyphCare.Users.Entities;

namespace ZyphCare.Users.Filters;

/// <summary>
/// Represents the order by which to order the results of a query.
/// </summary>
public class UserOrder<TProp> : OrderDefinition<User, TProp>
{
    /// <inheritdoc />
    public UserOrder()
    {
    }

    /// <inheritdoc />
    public UserOrder(Expression<Func<User, TProp>> keySelector, OrderDirection orderDirection)
    : base(keySelector, orderDirection)
    {
    }
}