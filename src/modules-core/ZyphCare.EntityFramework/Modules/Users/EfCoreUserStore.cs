using Microsoft.EntityFrameworkCore;
using Open.Linq.AsyncExtensions;
using ZyphCare.EntityFramework.Common;
using ZyphCare.EntityFramework.Common.Models;
using ZyphCare.Extensions;
using ZyphCare.Users.Contracts;
using ZyphCare.Users.Entities;
using ZyphCare.Users.Filters;

namespace ZyphCare.EntityFramework.Modules.Users;

/// <inheritdoc />
public class EfCoreUserStore : IUserEntityStore
{
    private readonly EntityStore<UserZyphCareDbContext, User> _store;
    
    /// <summary>
    /// Creates a new instance of the <see cref="EfCoreUserStore"/> instance.
    /// </summary>
    /// <param name="store">The <see cref="EntityStore{TDbContext,TEntity}"/></param>
    public EfCoreUserStore(EntityStore<UserZyphCareDbContext, User> store)
    {
        _store = store;
    }
    
    /// <inheritdoc />
    public async Task<User?> FindAsync(UserFilter definitionFilter,
        CancellationToken cancellationToken = default)
    {
        return await _store
            .QueryAsync(queryable => Filter(queryable, definitionFilter), cancellationToken)
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<User?> FindAsync<TOrderBy>(UserFilter filter,
        UserOrder<TOrderBy> order,
        CancellationToken cancellationToken = default)
    {
        return await _store.QueryAsync(queryable => Filter(queryable, filter).OrderBy(order), cancellationToken)
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<Page<User>> FindManyAsync(UserFilter filter, PageArgs pageArgs,
        CancellationToken cancellationToken = default)
    {
        var count = await _store.QueryAsync(queryable => Filter(queryable, filter), cancellationToken).LongCount();
        var results = await _store
            .QueryAsync(queryable => Paginate(Filter(queryable, filter), pageArgs), cancellationToken).ToList();
        return new(results, count);
    }

    /// <inheritdoc />
    public async Task<Page<User>> FindManyAsync<TOrderBy>(UserFilter filter,
        UserOrder<TOrderBy> order, PageArgs pageArgs,
        CancellationToken cancellationToken = default)
    {
        var count = await _store.QueryAsync(queryable => Filter(queryable, filter), cancellationToken).LongCount();
        var results = await _store.QueryAsync(queryable => Paginate(Filter(queryable, filter).OrderBy(order), pageArgs),
        cancellationToken).ToList();
        return new(results, count);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> FindManyAsync(UserFilter definitionFilter,
        CancellationToken cancellationToken = default)
    {
        return await _store.QueryAsync(queryable => Filter(queryable, definitionFilter), cancellationToken).ToList();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> FindManyAsync<TOrderBy>(UserFilter filter,
        UserOrder<TOrderBy> order,
        CancellationToken cancellationToken = default)
    {
        return await _store.QueryAsync(queryable => Filter(queryable, filter).OrderBy(order), cancellationToken)
            .ToList();
    }

    /// <inheritdoc />
    public async Task SaveAsync(User definition, CancellationToken cancellationToken = default)
    {
        await _store.SaveAsync(definition, cancellationToken);
    }

    /// <inheritdoc />
    public async Task SaveManyAsync(IEnumerable<User> definitions,
        CancellationToken cancellationToken = default)
    {
        await _store.SaveManyAsync(definitions, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<long> DeleteManyAsync(UserFilter definitionFilter,
        CancellationToken cancellationToken = default)
    {
        await using var dbContext = await _store.CreateDbContextAsync(cancellationToken);
        var set = dbContext.Users;
        var queryable = set.AsQueryable();
        var ids = await Filter(queryable, definitionFilter).Select(x => x.Id).Distinct().ToListAsync(cancellationToken);
        return await _store.DeleteWhereAsync(x => ids.Contains(x.Id), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(User definition, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await _store.CreateDbContextAsync(cancellationToken);
        return await _store.DeleteAsync(definition, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> AnyAsync(UserFilter definitionFilter,
        CancellationToken cancellationToken = default)
    {
        return await _store.QueryAsync(queryable => Filter(queryable, definitionFilter), cancellationToken).Any();
    }

    /// <inheritdoc />
    public async Task<long> CountAsync(UserFilter filter, CancellationToken cancellationToken = default)
    {
        return await _store.CountAsync(queryable => Filter(queryable, filter), cancellationToken);
    }

    private IQueryable<User> Filter(IQueryable<User> queryable,
        UserFilter definitionFilter)
    {
        return definitionFilter.Apply(queryable);
    }

    private IQueryable<User> Paginate(IQueryable<User> queryable, PageArgs? pageArgs)
    {
        if (pageArgs?.Offset != null) queryable = queryable.Skip(pageArgs.Offset.Value);
        if (pageArgs?.Limit != null) queryable = queryable.Take(pageArgs.Limit.Value);
        return queryable;
    }
}