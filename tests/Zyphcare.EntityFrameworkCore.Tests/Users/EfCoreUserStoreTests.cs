using FakeItEasy;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Open.Linq.AsyncExtensions;
using ZyphCare.Common.Entities;
using ZyphCare.Common.Models;
using ZyphCare.EntityFramework.Common;
using ZyphCare.EntityFramework.Common.Contracts;
using ZyphCare.EntityFramework.Units.Users;
using ZyphCare.Users.Entities;
using ZyphCare.Users.Filters;

namespace Zyphcare.EntityFrameworkCore.Tests.Users;

/// <summary>
/// Defines a test class for verifying the functionality and behavior of
/// the Entity Framework Core-based user store implementation.
/// </summary>
public class EfCoreUserStoreTests : IDisposable
{
   private readonly SqliteConnection _connection;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfCoreUserStoreTests"/> class
    /// </summary>
    public EfCoreUserStoreTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
    }

    /// <summary>
    /// Tests whether the <see cref="EfCoreUserStore.FindAsync"/> method correctly retrieves a valid user
    /// when provided with an appropriate filter.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation. The task result asserts that the correct user is returned
    /// and matches the expected user.
    /// </returns>
    [Fact]
    public async Task FindAsync_WithCorrectFilter_ReturnsValidUser()
    {
        // Arrange 
        var cancellationToken = CancellationToken.None;
        var userStore = await CreateStoreAsync(cancellationToken);
        var user = new User
            {
                Auth0Id = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                
            };
        var filter = new UserFilter { Auth0Id = user.Auth0Id };
        await userStore.SaveAsync(user, cancellationToken);

        // Act
        var foundUser = await userStore.FindAsync(filter, cancellationToken);

        // Assert
        foundUser.Should().BeEquivalentTo(user);
    }

    /// <summary>
    /// Tests <see cref="EfCoreUserStore.FindManyAsync(UserFilter, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task FindManyAsync_WithFilter_ReturnsUsers()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var userStore = await CreateStoreAsync(cancellationToken);
        var userList = CreateDummyUsers();
        var filter = new UserFilter { Ids = new List<string> { userList[0].Id, userList[1].Id } };
        await userStore.SaveManyAsync(userList, cancellationToken);

        // Act
        var foundUsers = await userStore.FindManyAsync(filter, cancellationToken).ToList();

        // Assert
        foundUsers.Should().NotBeNull();
        foundUsers.Should().HaveCount(2);
        foundUsers.Should().OnlyContain(u => filter.Ids.Contains(u.Id));

    }

    /// <summary>
    /// Tests <see cref="EfCoreUserStore.FindAsync{TOrderBy}(UserFilter, UserOrder{TOrderBy}, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task FindAsync_WithOrdering_ReturnsUserInOrder()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var userStore = await CreateStoreAsync(cancellationToken);
        var userOrder = new UserOrder<string>(u => u.Auth0Id, OrderDirection.Descending);
        var userList = CreateDummyUsers();
        await userStore.SaveManyAsync(userList, cancellationToken);

        // Act
        var foundUser = await userStore.FindAsync(new UserFilter(), userOrder, cancellationToken);

        // Assert
        foundUser.Should().NotBeNull();
        foundUser!.Auth0Id.Should().Be(userList.OrderByDescending(u => u.Auth0Id).First().Auth0Id);
    }

    /// <summary>
    /// Tests <see cref="EfCoreUserStore.FindManyAsync(UserFilter, PageArgs, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task FindManyAsync_WithFilterAndPageArgs_ReturnsPaginatedUsers()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var userStore = await CreateStoreAsync(cancellationToken);
        var userList = CreateDummyUsers();
        var pageArgs = new PageArgs { Offset = 1, Limit = 2 };
        var filter = new UserFilter { Ids = userList.Select(u => u.Id).ToList() };
        await userStore.SaveManyAsync(userList, cancellationToken);

        // Act
        var pageResult = await userStore.FindManyAsync(filter, pageArgs, cancellationToken);

        // Assert
        pageResult.Should().NotBeNull();
        pageResult.Items.Should().HaveCount(pageArgs.Limit.Value);
        pageResult.Items.Should().OnlyContain(x => filter.Ids.Contains(x.Id));
        pageResult.TotalCount.Should().Be(userList.Count);

    }

    /// <summary>
    /// Tests <see cref="EfCoreUserStore.FindManyAsync{TOrderBy}(UserFilter, UserOrder{TOrderBy}, PageArgs, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task FindManyAsync_WithFilterOrderingAndPagination_ReturnsOrderedPaginatedUsers()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var userStore = await CreateStoreAsync(cancellationToken);
        var userList = CreateDummyUsers();
        var order = new UserOrder<string>(u => u.Auth0Id, OrderDirection.Descending);
        var pageArgs = new PageArgs { Offset = 0, Limit = 2 };
        await userStore.SaveManyAsync(userList, cancellationToken);

        // Act
        var pageResult = await userStore.FindManyAsync(new UserFilter { Ids = new List<string> { userList[0].Id, userList[1].Id, userList[2].Id } }, order, pageArgs, cancellationToken);

        // Assert
        pageResult.Should().NotBeNull();
        pageResult.Items.Should().HaveCount(pageArgs.Limit.Value);
        pageResult.Items.Should().BeInDescendingOrder(x => x.Auth0Id);
        pageResult.TotalCount.Should().Be(userList.Count);
    }

    /// <summary>
    /// Tests <see cref="EfCoreUserStore.DeleteAsync(User, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_WithExistingUser_DeletesUser()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var userStore = await CreateStoreAsync(cancellationToken);
        var user = new User
            {
                Auth0Id = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
            };
        await userStore.SaveAsync(user, cancellationToken);

        // Act
        var result = await userStore.DeleteAsync(user, cancellationToken);

        // Assert
        result.Should().BeTrue();
        var deletedUser = await userStore.FindAsync(new UserFilter { Auth0Id = user.Auth0Id }, cancellationToken);
        deletedUser.Should().BeNull();
    }

    /// <summary>
    /// Tests <see cref="EfCoreUserStore.CountAsync(UserFilter, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task CountAsync_WithFilter_ReturnsCorrectCount()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var userStore = await CreateStoreAsync(cancellationToken);
        var userList = CreateDummyUsers();
        var filter = new UserFilter { Ids = [userList[0].Id, userList[1].Id]};
        await userStore.SaveManyAsync(userList, cancellationToken);

        // Act
        var result = await userStore.CountAsync(filter, cancellationToken);

        // Assert
        result.Should().Be(userList.Count(u => filter.Id != null && filter.Id.Contains(u.Id)));
    }

    /// <summary>
    /// Tests <see cref="EfCoreUserStore.SaveAsync(User, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task SaveAsync_WithValidUser_SavesUserSuccessfully()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var userStore = await CreateStoreAsync(cancellationToken);
        var user = new User
            {
                Auth0Id = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
            };

        // Act
        await userStore.SaveAsync(user, cancellationToken);

        // Assert
        var savedUser = await userStore.FindAsync(new UserFilter { Auth0Id = user.Auth0Id }, cancellationToken);
        savedUser.Should().BeEquivalentTo(user);
    }

    /// <summary>
    /// Tests <see cref="EfCoreUserStore.SaveManyAsync(IEnumerable{User}, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task SaveManyAsync_WithValidUsers_SavesUsersSuccessfully()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var userStore = await CreateStoreAsync(cancellationToken);
        var userList = CreateDummyUsers();

        // Act
        await userStore.SaveManyAsync(userList, cancellationToken);

        // Assert
        var savedUsers = await userStore.FindManyAsync(new UserFilter(), cancellationToken).ToList();
        savedUsers.Should().HaveCount(userList.Count);
        savedUsers.Should().BeEquivalentTo(userList);
    }

    /// <summary>
    /// Tests <see cref="EfCoreUserStore.AnyAsync(UserFilter, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task AnyAsync_WithExistingFilter_ReturnsTrue()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var userStore = await CreateStoreAsync(cancellationToken);
        var user = new User
            {
                Auth0Id = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
            };
        await userStore.SaveAsync(user, cancellationToken);

        // Act
        var result = await userStore.AnyAsync(new UserFilter { Auth0Id = user.Auth0Id }, cancellationToken);

        // Assert
        result.Should().BeTrue();
    }

    private static List<User> CreateDummyUsers()
    {
        return
            [
                new User
                    {
                        Auth0Id = Guid.NewGuid().ToString(),
                        Id = Guid.NewGuid().ToString(),
                    },

                new User
                    {
                        Auth0Id = Guid.NewGuid().ToString(),
                        Id = Guid.NewGuid().ToString(),
                    },

                new User
                    {
                        Auth0Id = Guid.NewGuid().ToString(),
                        Id = Guid.NewGuid().ToString(),
                    }
            ];
    }

    private async Task<EfCoreUserStore> CreateStoreAsync(CancellationToken cancellationToken)
    {
        var options = new DbContextOptionsBuilder<UserZyphCareDbContext>()
            .UseSqlite(_connection).Options;

        await using var context = new UserZyphCareDbContext(options);

        await context.Database.EnsureCreatedAsync(cancellationToken);
        var dbContextFactory = A.Fake<IDbContextFactory<UserZyphCareDbContext>>();
        var dbExceptionHandler = A.Fake<IDbExceptionHandler<UserZyphCareDbContext>>();

        A.CallTo(() => dbContextFactory.CreateDbContextAsync(A<CancellationToken>._))
            .ReturnsLazily(() => new UserZyphCareDbContext(options));

        var entityStore = new EntityStore<UserZyphCareDbContext, User>(dbContextFactory, dbExceptionHandler);
        return new EfCoreUserStore(entityStore);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _connection.Dispose();
    }
}
