using Open.Linq.AsyncExtensions;
using ZyphCare.Common.Models;
using ZyphCare.EntityFramework.Common;
using ZyphCare.Extensions;
using ZyphCare.HealthRecords.Contracts;
using ZyphCare.HealthRecords.Entities;
using ZyphCare.HealthRecords.Filters;

namespace ZyphCare.EntityFramework.Units.HealthRecords;

/// <inheritdoc />
public class EfCoreHealthRecordStore : IHealthRecordEntityStore
{
    private readonly EntityStore<HealthRecordZyphCareDbContext, HealthRecord> _store;

    /// <summary>
    /// A store implementation for managing health records using Entity Framework Core.
    /// </summary>
    public EfCoreHealthRecordStore(EntityStore<HealthRecordZyphCareDbContext, HealthRecord> store)
    {
        _store = store;
    }

    /// <inheritdoc />
    public async Task<HealthRecord?> FindAsync(HealthRecordFilter healthRecordFilter, CancellationToken cancellationToken = default)
    {
        return await _store
            .QueryAsync(queryable => Filter(queryable, healthRecordFilter), cancellationToken)
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<HealthRecord?> FindAsync<TOrderBy>(HealthRecordFilter healthRecordFilter, HealthRecordOrder<TOrderBy> order, CancellationToken cancellationToken = default)
    {
        return await _store
            .QueryAsync(queryable => Filter(queryable, healthRecordFilter).OrderBy(order), cancellationToken)
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<Page<HealthRecord>> FindManyAsync(HealthRecordFilter healthRecordFilter, PageArgs pageArgs, CancellationToken cancellationToken = default)
    {
        var count = await _store.QueryAsync(queryable => Filter(queryable, healthRecordFilter), cancellationToken).LongCount();
        var results = await _store
            .QueryAsync(queryable => Paginate(Filter(queryable, healthRecordFilter), pageArgs), cancellationToken).ToList();
        return new(results, count);
    }

    /// <inheritdoc />
    public async Task<Page<HealthRecord>> FindManyAsync<TOrderBy>(HealthRecordFilter healthRecordFilter, HealthRecordOrder<TOrderBy> order, PageArgs pageArgs, CancellationToken cancellationToken = default)
    {
        var count = await _store.QueryAsync(queryable => Filter(queryable, healthRecordFilter), cancellationToken).LongCount();
        var results = await _store.QueryAsync(queryable => Paginate(Filter(queryable, healthRecordFilter).OrderBy(order), pageArgs), cancellationToken).ToList();
        return new(results, count);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<HealthRecord>> FindManyAsync(HealthRecordFilter healthRecordFilter, CancellationToken cancellationToken = default)
    {
        return await _store.QueryAsync(queryable => Filter(queryable, healthRecordFilter), cancellationToken).ToList();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<HealthRecord>> FindManyAsync<TOrderBy>(HealthRecordFilter healthRecordFilter, HealthRecordOrder<TOrderBy> order, CancellationToken cancellationToken = default)
    {
       return await _store.QueryAsync(queryable => Filter(queryable, healthRecordFilter).OrderBy(order), cancellationToken).ToList();
    }

    /// <inheritdoc />
    public async Task SaveAsync(HealthRecord healthRecord, CancellationToken cancellationToken = default)
    {
       await _store.SaveAsync(healthRecord, cancellationToken);
    }

    /// <inheritdoc />
    public async Task SaveManyAsync(IEnumerable<HealthRecord> healthRecords, CancellationToken cancellationToken = default)
    {
        await _store.SaveManyAsync(healthRecords, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(HealthRecord healthRecord, CancellationToken cancellationToken = default)
    {
       return await _store.DeleteAsync(healthRecord, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> AnyAsync(HealthRecordFilter healthRecordFilter, CancellationToken cancellationToken = default)
    {
       return await _store.QueryAsync(queryable => Filter(queryable, healthRecordFilter), cancellationToken).Any();
    }

    /// <inheritdoc />
    public async Task<long> CountAsync(HealthRecordFilter filter, CancellationToken cancellationToken = default)
    {
        return await _store.CountAsync(queryable => Filter(queryable, filter), cancellationToken);
    }

    private IQueryable<HealthRecord> Filter(IQueryable<HealthRecord> queryable, HealthRecordFilter filter)
    {
        return filter.Apply(queryable);
    }

    private IQueryable<HealthRecord> Paginate(IQueryable<HealthRecord> queryable, PageArgs? pageArgs)
    {
        if (pageArgs?.Offset != null) queryable = queryable.Skip(pageArgs.Offset.Value);
        if (pageArgs?.Limit != null) queryable = queryable.Take(pageArgs.Limit.Value);
        return queryable;
    }
}