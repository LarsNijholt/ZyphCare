using ZyphCare.EntityFramework.Common.Models;
using ZyphCare.Users.Contracts;
using ZyphCare.Users.Entities;
using ZyphCare.Users.Filters;

namespace ZyphCare.Users.Stores;

public class MemoryUserEntityStore : IUserEntityStore
{

    public async Task<User?> FindAsync(UserFilter definitionFilter, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task<User?> FindAsync<TOrderBy>(UserFilter filter, UserOrder<TOrderBy> order, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task<Page<User>> FindManyAsync(UserFilter filter, PageArgs pageArgs, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task<Page<User>> FindManyAsync<TOrderBy>(UserFilter filter, UserOrder<TOrderBy> order, PageArgs pageArgs, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task<IEnumerable<User>> FindManyAsync(UserFilter definitionFilter, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task<IEnumerable<User>> FindManyAsync<TOrderBy>(UserFilter filter, UserOrder<TOrderBy> order, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task SaveAsync(User definition, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task SaveManyAsync(IEnumerable<User> definitions, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task<bool> DeleteAsync(User definition, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task<long> DeleteManyAsync(UserFilter definitionFilter, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task<bool> AnyAsync(UserFilter definitionFilter, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task<long> CountAsync(UserFilter filter, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}