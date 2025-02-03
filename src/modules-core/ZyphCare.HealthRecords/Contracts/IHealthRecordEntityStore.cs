using ZyphCare.Common.Models;
using ZyphCare.HealthRecords.Entities;
using ZyphCare.HealthRecords.Filters;
using ZyphCare.HealthRecords.Models;

namespace ZyphCare.HealthRecords.Contracts;

/// <summary>
/// Provides methods to interact with the health record entity store, including functionalities
/// for retrieving, saving, deleting, and counting health records based on various criteria.
/// </summary>
public interface IHealthRecordEntityStore
{
     /// <summary>
    /// Finds a user using the specified userFilter.
    /// </summary>
    /// <param name="healthRecordFilter">the userFilter.</param>
    /// <param name="cancellationToken">the cancellation token.</param>
    Task<HealthRecord?> FindAsync(HealthRecordFilter healthRecordFilter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds a user using the specified filter and order.
    /// </summary>
    /// <param name="healthRecordFilter">The filter.</param>
    /// <param name="order">The order.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
    /// <returns>The user user.</returns>
    Task<HealthRecord?> FindAsync<TOrderBy>(HealthRecordFilter healthRecordFilter, HealthRecordOrder<TOrderBy> order,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a paginated list of user users using the specified filter.
    /// </summary>
    /// <param name="healthRecordFilter">The filter.</param>
    /// <param name="pageArgs">The page arguments.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated list of user users.</returns>
    Task<Page<HealthRecord>> FindManyAsync(HealthRecordFilter healthRecordFilter, PageArgs pageArgs,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a paginated list of user users using the specified filter and order.
    /// </summary>
    /// <param name="healthRecordFilter">The filter.</param>
    /// <param name="order">The order.</param>
    /// <param name="pageArgs">The page arguments.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
    /// <returns>A paginated list of user users.</returns>
    Task<Page<HealthRecord>> FindManyAsync<TOrderBy>(HealthRecordFilter healthRecordFilter,
        HealthRecordOrder<TOrderBy> order, PageArgs pageArgs, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds a collection of user users using the specified userFilter.
    /// </summary>
    /// <param name="userFilter">the userFilter.</param>
    /// <param name="cancellationToken">the cancellation token.</param>
    Task<IEnumerable<HealthRecord>> FindManyAsync(HealthRecordFilter userFilter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a list of user users using the specified filter and order.
    /// </summary>
    /// <param name="healthRecordFilter">The filter.</param>
    /// <param name="order">The order.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
    /// <returns>A list of user users.</returns>
    Task<IEnumerable<HealthRecord>> FindManyAsync<TOrderBy>(HealthRecordFilter healthRecordFilter,
        HealthRecordOrder<TOrderBy> order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves the specified user user.
    /// </summary>
    /// <param name="healthRecords">The user file to save.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task SaveAsync(HealthRecord healthRecords, CancellationToken cancellationToken = default);

    /// <summary>
    /// Save a collection of user users.
    /// </summary>
    /// <param name="healthRecords">The user files to save.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task SaveManyAsync(IEnumerable<HealthRecord> healthRecords, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a user.
    /// </summary>
    Task<bool> DeleteAsync(HealthRecord healthRecord, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns true if any of the user users match the userFilter.
    /// </summary>
    /// <param name="healthRecordFilter">The userFilter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> AnyAsync(HealthRecord healthRecordFilter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts the total number of User entities that match the specified filter criteria.
    /// </summary>
    Task<long> CountAsync(HealthRecord filter, CancellationToken cancellationToken = default);
}