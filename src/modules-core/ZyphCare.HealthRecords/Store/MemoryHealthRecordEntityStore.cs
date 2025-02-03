using ZyphCare.Common.Models;
using ZyphCare.EntityFramework.Common.Services;
using ZyphCare.Extensions;
using ZyphCare.HealthRecords.Contracts;
using ZyphCare.HealthRecords.Entities;
using ZyphCare.HealthRecords.Filters;
using ZyphCare.HealthRecords.Models;

namespace ZyphCare.HealthRecords.Store;

/// <summary>
/// Provides an in-memory implementation of the <see cref="IHealthRecordEntityStore"/> interface,
/// enabling operations on health records such as querying, saving, and deleting entities.
/// It is designed for managing health record data without connecting to an external persistence layer.
/// </summary>
public class MemoryHealthRecordEntityStore : IHealthRecordEntityStore
{
    private readonly MemoryStore<HealthRecord> _store;

    /// <summary>
    /// Represents an in-memory implementation of the IHealthRecordEntityStore interface.
    /// Facilitates operations for managing health record entities such as querying,
    /// saving, deleting, and retrieving details without requiring an external database or persistence layer.
    /// </summary>
    public MemoryHealthRecordEntityStore(MemoryStore<HealthRecord> store)
    {
        _store = store;
    }

    /// <inheritdoc />
    public Task<HealthRecord?> FindAsync(HealthRecordFilter healthRecordFilter, CancellationToken cancellationToken = default)
    {
        var result = _store.Query(queryable => Filter(queryable, healthRecordFilter)).FirstOrDefault();
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<HealthRecord?> FindAsync<TOrderBy>(HealthRecordFilter healthRecordFilter, HealthRecordOrder<TOrderBy> order, CancellationToken cancellationToken = default)
    {
        var result = _store.Query(queryable => Filter(queryable, healthRecordFilter).OrderBy(order)).FirstOrDefault();
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<Page<HealthRecord>> FindManyAsync(HealthRecordFilter healthRecordFilter, PageArgs pageArgs, CancellationToken cancellationToken = default)
    {
        var count = _store.Query(query => Filter(query, healthRecordFilter)).LongCount();
        var result = _store.Query(query => Filter(query, healthRecordFilter).Paginate(pageArgs)).ToList();
        return Task.FromResult(Page.Of(result, count));
    }

    /// <inheritdoc />
    public Task<Page<HealthRecord>> FindManyAsync<TOrderBy>(HealthRecordFilter healthRecordFilter, HealthRecordOrder<TOrderBy> order, PageArgs pageArgs, CancellationToken cancellationToken = default)
    {
        var count = _store.Query(query => Filter(query, healthRecordFilter)).LongCount();
        var result = _store.Query(query => Filter(query, healthRecordFilter).OrderBy(order).Paginate(pageArgs)).ToList();
        return Task.FromResult(Page.Of(result, count));
    }

    /// <inheritdoc />
    public Task<IEnumerable<HealthRecord>> FindManyAsync(HealthRecordFilter healthRecordFilter, CancellationToken cancellationToken = default)
    {
        var result = _store.Query(queryable => Filter(queryable, healthRecordFilter)).ToList().AsEnumerable();
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<IEnumerable<HealthRecord>> FindManyAsync<TOrderBy>(HealthRecordFilter healthRecordFilter, HealthRecordOrder<TOrderBy> order, CancellationToken cancellationToken = default)
    {
        var result = _store.Query(query => Filter(query, healthRecordFilter).OrderBy(order)).ToList().AsEnumerable();
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task SaveAsync(HealthRecord healthRecords, CancellationToken cancellationToken = default)
    {
        _store.Save(healthRecords, GetId);
        return Task.CompletedTask;
    }

    public Task SaveManyAsync(IEnumerable<HealthRecord> healthRecords, CancellationToken cancellationToken = default)
    {
        _store.SaveMany(healthRecords, GetId);
        return Task.CompletedTask;
    }
    
    public Task<bool> DeleteAsync(HealthRecord healthRecord, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<bool> AnyAsync(HealthRecord healthRecordFilter, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<long> CountAsync(HealthRecord filter, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    
    private static IQueryable<HealthRecord> Filter(IQueryable<HealthRecord> queryable, HealthRecordFilter filter) => filter.Apply(queryable);
    private static string GetId(HealthRecord healthRecord) => healthRecord.Id;
    
}