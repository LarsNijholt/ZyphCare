using ZyphCare.Common.Entities;
using ZyphCare.EntityFramework.Common.Entities;

// ReSharper disable once CheckNamespace
namespace ZyphCare.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IQueryable{T}"/> objects.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Orders the queryable by the specified order.
    /// </summary>
    /// <param name="queryable">The queryable to order.</param>
    /// <param name="order">The order to apply to the queryable.</param>
    /// <typeparam name="T">The type of the queryable.</typeparam>
    /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
    /// <returns>The ordered queryable.</returns>
    public static IQueryable<T> OrderBy<T, TOrderBy>(this IQueryable<T> queryable, OrderDefinition<T, TOrderBy> order) =>
        order.Direction == OrderDirection.Ascending 
            ? queryable.OrderBy(order.KeySelector) 
            : queryable.OrderByDescending(order.KeySelector);
}