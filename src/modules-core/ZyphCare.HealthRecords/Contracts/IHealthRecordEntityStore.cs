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
    /// Finds a HealthRecord using the specified HealthRecordFilter.
    /// </summary>
    /// <param name="healthRecordFilter">the HealthRecordFilter.</param>
    /// <param name="cancellationToken">the cancellation token.</param>
    Task<HealthRecord?> FindAsync(HealthRecordFilter healthRecordFilter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds a HealthRecord using the specified filter and order.
    /// </summary>
    /// <param name="healthRecordFilter">The filter.</param>
    /// <param name="order">The order.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
    /// <returns>The HealthRecord HealthRecord.</returns>
    Task<HealthRecord?> FindAsync<TOrderBy>(HealthRecordFilter healthRecordFilter, HealthRecordOrder<TOrderBy> order,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a paginated list of HealthRecords using the specified filter.
    /// </summary>
    /// <param name="healthRecordFilter">The filter.</param>
    /// <param name="pageArgs">The page arguments.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated list of HealthRecord HealthRecords.</returns>
    Task<Page<HealthRecord>> FindManyAsync(HealthRecordFilter healthRecordFilter, PageArgs pageArgs,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a paginated list of HealthRecord using the specified filter and order.
    /// </summary>
    /// <param name="healthRecordFilter">The filter.</param>
    /// <param name="order">The order.</param>
    /// <param name="pageArgs">The page arguments.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
    /// <returns>A paginated list of HealthRecord HealthRecords.</returns>
    Task<Page<HealthRecord>> FindManyAsync<TOrderBy>(HealthRecordFilter healthRecordFilter,
        HealthRecordOrder<TOrderBy> order, PageArgs pageArgs, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds a collection of HealthRecord using the specified HealthRecordFilter.
    /// </summary>
    /// <param name="healthRecordFilter">the HealthRecordFilter.</param>
    /// <param name="cancellationToken">the cancellation token.</param>
    Task<IEnumerable<HealthRecord>> FindManyAsync(HealthRecordFilter healthRecordFilter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a list of HealthRecords using the specified filter and order.
    /// </summary>
    /// <param name="healthRecordFilter">The filter.</param>
    /// <param name="order">The order.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <typeparam name="TOrderBy">The type of the property to order by.</typeparam>
    /// <returns>A list of HealthRecord HealthRecords.</returns>
    Task<IEnumerable<HealthRecord>> FindManyAsync<TOrderBy>(HealthRecordFilter healthRecordFilter,
        HealthRecordOrder<TOrderBy> order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves the specified HealthRecord.
    /// </summary>
    /// <param name="healthRecord">The HealthRecord file to save.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task SaveAsync(HealthRecord healthRecord, CancellationToken cancellationToken = default);

    /// <summary>
    /// Save a collection of HealthRecords.
    /// </summary>
    /// <param name="healthRecords">The HealthRecord files to save.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task SaveManyAsync(IEnumerable<HealthRecord> healthRecords, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a HealthRecord.
    /// </summary>
    Task<bool> DeleteAsync(HealthRecord healthRecord, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns true if any of the HealthRecord HealthRecords match the HealthRecordFilter.
    /// </summary>
    /// <param name="healthRecordFilter">The HealthRecordFilter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> AnyAsync(HealthRecordFilter healthRecordFilter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts the total number of HealthRecord entities that match the specified filter criteria.
    /// </summary>
    Task<long> CountAsync(HealthRecordFilter filter, CancellationToken cancellationToken = default);
}