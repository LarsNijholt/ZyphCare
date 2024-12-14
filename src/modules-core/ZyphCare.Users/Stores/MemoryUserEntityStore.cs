using ZyphCare.Common.Models;
using ZyphCare.Extensions;
using ZyphCare.EntityFramework.Common.Services;
using ZyphCare.Users.Contracts;
using ZyphCare.Users.Entities;
using ZyphCare.Users.Filters;

namespace ZyphCare.Users.Stores;

/// <summary>
/// Provides memory access to stored data set definitions.
/// </summary>
public class MemoryUserEntityStore : IUserEntityStore
{
    private readonly MemoryStore<User> _store;

    /// <summary>
    /// Represents an in-memory implementation of the <see cref="IUserEntityStore"/> interface,
    /// providing functionality for managing and querying user entities stored in memory.
    /// </summary>
    public MemoryUserEntityStore(MemoryStore<User> store)
    {
        _store = store;
    }

    /// <inheritdoc />
    public Task<User?> FindAsync(UserFilter userFilter, CancellationToken cancellationToken = default)
    {
        var result = _store.Query(queryable => Filter(queryable, userFilter)).FirstOrDefault();
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<User?> FindAsync<TOrderBy>(UserFilter filter, UserOrder<TOrderBy> order, CancellationToken cancellationToken = default)
    {
        var result = _store.Query(queryable => Filter(queryable, filter).OrderBy(order)).FirstOrDefault();
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<Page<User>> FindManyAsync(UserFilter filter, PageArgs pageArgs, CancellationToken cancellationToken = default)
    {
        var count = _store.Query(query => Filter(query, filter)).LongCount();
        var result = _store.Query(query => Filter(query, filter).Paginate(pageArgs)).ToList();
        return Task.FromResult(Page.Of(result, count));
    }
    
    /// <inheritdoc />
    public Task<Page<User>> FindManyAsync<TOrderBy>(UserFilter filter, UserOrder<TOrderBy> order, PageArgs pageArgs, CancellationToken cancellationToken = default)
    {
        var count = _store.Query(query => Filter(query, filter)).LongCount();
        var result = _store.Query(query => Filter(query, filter).OrderBy(order).Paginate(pageArgs)).ToList();
        return Task.FromResult(Page.Of(result, count));
    }

    /// <inheritdoc />
    public Task<IEnumerable<User>> FindManyAsync(UserFilter definitionFilter, CancellationToken cancellationToken = default)
    {
        var result = _store.Query(queryable => Filter(queryable, definitionFilter)).ToList().AsEnumerable();
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<IEnumerable<User>> FindManyAsync<TOrderBy>(UserFilter filter, UserOrder<TOrderBy> order, CancellationToken cancellationToken = default)
    {
        var result = _store.Query(query => Filter(query, filter).OrderBy(order)).ToList().AsEnumerable();
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task SaveAsync(User user, CancellationToken cancellationToken = default)
    {
        _store.Save(user, GetId);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task SaveManyAsync(IEnumerable<User> users, CancellationToken cancellationToken = default)
    {
        _store.SaveMany(users, GetId);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        var completed = _store.Delete(user.Id);
        return Task.FromResult(completed);
    }

    /// <inheritdoc />
    public Task<bool> AnyAsync(UserFilter userFilter, CancellationToken cancellationToken = default)
    {
        var exists = _store.Query(queryable => Filter(queryable, userFilter)).Any();
        return Task.FromResult(exists);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(UserFilter filter, CancellationToken cancellationToken = default)
    {
        // The memory store does not support CountAsync with a filter, so we need to use Query with a LongCount instead.
        return Task.FromResult(_store.Query(queryable => Filter(queryable, filter)).LongCount());
    }

    private static IQueryable<User> Filter(IQueryable<User> queryable, UserFilter filter) => filter.Apply(queryable);
    private static string GetId(User user) => user.Id;
}