using ZyphCare.EntityFramework.Common.Models;
using ZyphCare.Users.Entities;
using ZyphCare.Users.Filters;

namespace ZyphCare.Users.Contracts;

/// <summary>
/// Provides access to stored user entities.
/// </summary>
public interface IUserEntityStore
{
    /// <summary>
    /// Finds a user definition using the specified definitionFilter.
    /// </summary>
    /// <param name="definitionFilter">the definitionFilter.</param>
    /// <param name="cancellationToken">the cancellation token.</param>
    Task<User?> FindAsync(UserFilter definitionFilter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds a user definition using the specified filter and order.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="order">The order.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
    /// <returns>The user definition.</returns>
    Task<User?> FindAsync<TOrderBy>(UserFilter filter, UserOrder<TOrderBy> order,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a paginated list of user definitions using the specified filter.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="pageArgs">The page arguments.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated list of user definitions.</returns>
    Task<Page<User>> FindManyAsync(UserFilter filter, PageArgs pageArgs,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a paginated list of user definitions using the specified filter and order.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="order">The order.</param>
    /// <param name="pageArgs">The page arguments.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
    /// <returns>A paginated list of user definitions.</returns>
    Task<Page<User>> FindManyAsync<TOrderBy>(UserFilter filter,
        UserOrder<TOrderBy> order, PageArgs pageArgs, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds a collection of user definitions using the specified definitionFilter.
    /// </summary>
    /// <param name="definitionFilter">the definitionFilter.</param>
    /// <param name="cancellationToken">the cancellation token.</param>
    Task<IEnumerable<User>> FindManyAsync(UserFilter definitionFilter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a list of user definitions using the specified filter and order.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="order">The order.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
    /// <returns>A list of user definitions.</returns>
    Task<IEnumerable<User>> FindManyAsync<TOrderBy>(UserFilter filter,
        UserOrder<TOrderBy> order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves the specified user definition.
    /// </summary>
    /// <param name="definition">The user file to save.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task SaveAsync(User definition, CancellationToken cancellationToken = default);

    /// <summary>
    /// Save a collection of user definitions.
    /// </summary>
    /// <param name="definitions">The user files to save.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task SaveManyAsync(IEnumerable<User> definitions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a user.
    /// </summary>
    Task<bool> DeleteAsync(User definition, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes all user definitions matching the specified definitionFilter.
    /// </summary>
    /// <param name="definitionFilter">The definitionFilter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<long> DeleteManyAsync(UserFilter definitionFilter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns true if any of the user definitions match the definitionFilter.
    /// </summary>
    /// <param name="definitionFilter">The definitionFilter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> AnyAsync(UserFilter definitionFilter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts the total number of User entities that match the specified filter criteria.
    /// </summary>
    Task<long> CountAsync(UserFilter filter, CancellationToken cancellationToken = default);
}