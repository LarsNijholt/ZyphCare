using FluentAssertions;
using ZyphCare.Common.Entities;
using ZyphCare.Common.Models;
using ZyphCare.EntityFramework.Common.Services;
using ZyphCare.Users.Entities;
using ZyphCare.Users.Filters;
using ZyphCare.Users.Stores;

namespace ZyphCare.Users.Tests.Stores;

/// <summary>
/// Provides unit tests for the <see cref="MemoryUserEntityStore"/> class, which include
/// functionality such as finding, saving, deleting, and querying user data in a memory-based store.
/// </summary>
public class MemoryUserEntityStoreTests
{
    /// <summary>
    /// Tests <see cref="MemoryUserEntityStore.FindAsync(UserFilter, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task FindAsync_WithCorrectFilter_ReturnsValidUser()
    {
        // Arrange
        var memoryStore = new MemoryStore<User>();
        var userEntityStore = new MemoryUserEntityStore(memoryStore);
        var user = new User
            {
                Auth0Id = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                Role = "Patient"
            };
        memoryStore.Save(user, u => u.Id);

        var filter = new UserFilter { Auth0Id = user.Auth0Id };

        // Act
        var result = await userEntityStore.FindAsync(filter);

        // Assert
        result.Should().BeEquivalentTo(user);
    }
    
    /// <summary>
    /// Tests <see cref="MemoryUserEntityStore.FindAsync{TOrderBy}(UserFilter, UserOrder{TOrderBy}, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task FindAsync_WithFilterAndOrdering_ReturnsOrderedUser()
    {
        // Arrange
        var memoryStore = new MemoryStore<User>();
        var userEntityStore = new MemoryUserEntityStore(memoryStore);
        var userList = CreateDummyUsers();
        memoryStore.SaveMany(userList, u => u.Id);

        var filter = new UserFilter { Role = "Patient" };
        var order = new UserOrder<string>(u => u.Auth0Id, OrderDirection.Descending);

        // Act
        var result = await userEntityStore.FindAsync(filter, order);

        // Assert
        result.Should().NotBeNull();
        result!.Auth0Id.Should().Be(userList.Where(u => u.Role == "Patient").OrderByDescending(u => u.Auth0Id).First().Auth0Id);
    }
    
    /// <summary>
    /// Tests <see cref="MemoryUserEntityStore.FindManyAsync(UserFilter, PageArgs, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task FindManyAsync_WithFilterAndPagination_ReturnsPaginatedUsers()
    {
        // Arrange
        var memoryStore = new MemoryStore<User>();
        var userEntityStore = new MemoryUserEntityStore(memoryStore);
        var userList = CreateDummyUsers();
        memoryStore.SaveMany(userList, u => u.Id);

        var filter = new UserFilter { Roles = new List<string> { "Patient", "Doctor" } };
        var pageArgs = new PageArgs { Offset = 0, Limit = 2 };

        // Act
        var pageResult = await userEntityStore.FindManyAsync(filter, pageArgs);

        // Assert
        pageResult.Should().NotBeNull();
        pageResult.Items.Should().HaveCount(pageArgs.Limit.Value);
        pageResult.Items.Should().OnlyContain(u => filter.Roles.Contains(u.Role));
        pageResult.TotalCount.Should().Be(userList.Count(u => filter.Roles.Contains(u.Role)));
    }
    
    /// <summary>
    /// Tests <see cref="MemoryUserEntityStore.FindManyAsync{TOrderBy}(UserFilter, UserOrder{TOrderBy}, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task FindManyAsync_WithFilterAndOrdering_ReturnsOrderedUsers()
    {
        // Arrange
        var memoryStore = new MemoryStore<User>();
        var userEntityStore = new MemoryUserEntityStore(memoryStore);
        var userList = CreateDummyUsers();

        // Save users to memory store
        memoryStore.SaveMany(userList, u => u.Id);

        // Define filter and ordering
        var filter = new UserFilter { Roles = new List<string> { "Patient", "Doctor" } };
        var order = new UserOrder<string>(u => u.Auth0Id, OrderDirection.Ascending);

        // Act
        var result = await userEntityStore.FindManyAsync(filter, order);

        // Assert
        var resultAsList = result.ToList();
        resultAsList.Should().NotBeNull();
        resultAsList.Should().HaveCount(resultAsList.Count(u => filter.Roles.Contains(u.Role)));
        resultAsList.Should().BeInAscendingOrder(u => u.Auth0Id);
    }
    
    /// <summary>
    /// Tests <see cref="MemoryUserEntityStore.FindManyAsync{TOrderBy}(UserFilter, UserOrder{TOrderBy}, PageArgs, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task FindManyAsync_WithFilterOrderingAndPagination_ReturnsOrderedPaginatedUsers()
    {
        // Arrange
        var memoryStore = new MemoryStore<User>();
        var userEntityStore = new MemoryUserEntityStore(memoryStore);
        var userList = CreateDummyUsers();
        memoryStore.SaveMany(userList, u => u.Id);

        var filter = new UserFilter { Roles = new List<string> { "Patient", "Doctor" } };
        var order = new UserOrder<string>(u => u.Auth0Id, OrderDirection.Ascending);
        var pageArgs = new PageArgs { Offset = 0, Limit = 2 };

        // Act
        var pageResult = await userEntityStore.FindManyAsync(filter, order, pageArgs);

        // Assert
        pageResult.Should().NotBeNull();
        pageResult.Items.Should().HaveCount(pageArgs.Limit.Value);
        pageResult.Items.Should().BeInAscendingOrder(u => u.Auth0Id);
        pageResult.TotalCount.Should().Be(userList.Count(u => filter.Roles.Contains(u.Role)));
    }
    
    /// <summary>
    /// Tests <see cref="MemoryUserEntityStore.SaveAsync(User, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task SaveAsync_WithValidUser_SavesUserSuccessfully()
    {
        // Arrange
        var memoryStore = new MemoryStore<User>();
        var userEntityStore = new MemoryUserEntityStore(memoryStore);
        var user = new User
            {
                Auth0Id = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                Role = "Patient"
            };

        // Act
        await userEntityStore.SaveAsync(user, CancellationToken.None);

        // Assert
        var response = await userEntityStore.FindAsync(new UserFilter { Id = user.Id }, CancellationToken.None);
        response.Should().BeEquivalentTo(user);
    }
    
    /// <summary>
    /// Tests <see cref="MemoryUserEntityStore.SaveManyAsync(IEnumerable{User}, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task SaveManyAsync_WithValidUsers_SavesUsersSuccessfully()
    {
        // Arrange
        var memoryStore = new MemoryStore<User>();
        var userEntityStore = new MemoryUserEntityStore(memoryStore);
        var userList = CreateDummyUsers();

        // Act
        await userEntityStore.SaveManyAsync(userList);

        // Assert
        var response = await userEntityStore.FindManyAsync(new UserFilter(), CancellationToken.None);
        var responseIds = response.Select(x => x.Id).ToList();
        userList.Should().OnlyContain(x => responseIds.Contains(x.Id));
    }
    
    /// <summary>
    /// Tests <see cref="MemoryUserEntityStore.DeleteAsync(User, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_WithExistingUser_DeletesUser()
    {
        // Arrange
        var memoryStore = new MemoryStore<User>();
        var userEntityStore = new MemoryUserEntityStore(memoryStore);
        var user = new User
            {
                Auth0Id = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                Role = "Patient"
            };
        memoryStore.Save(user, u => u.Id);

        // Act
        var result = await userEntityStore.DeleteAsync(user);

        // Assert
        result.Should().BeTrue();
        var queryResult = await userEntityStore.FindManyAsync(new UserFilter(), CancellationToken.None);
        queryResult.Should().BeEmpty();
    }
    
    /// <summary>
    /// Tests <see cref="MemoryUserEntityStore.AnyAsync(UserFilter, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task AnyAsync_WithFilter_ReturnsTrueWhenExists()
    {
        // Arrange
        var memoryStore = new MemoryStore<User>();
        var userEntityStore = new MemoryUserEntityStore(memoryStore);
        var userList = CreateDummyUsers();

        // Save users to memory store
        memoryStore.SaveMany(userList, u => u.Id);

        // Define filter to target specific role
        var filter = new UserFilter { Role = "Patient" };

        // Act
        var exists = await userEntityStore.AnyAsync(filter);

        // Assert
        exists.Should().BeTrue();
    }

    /// <summary>
    /// Tests <see cref="MemoryUserEntityStore.AnyAsync(UserFilter, CancellationToken)" /> to ensure it
    /// returns false when no matching entity exists for the given filter.
    /// </summary>
    [Fact]
    public async Task AnyAsync_WithFilter_ReturnsFalseWhenNotExists()
    {
        // Arrange
        var memoryStore = new MemoryStore<User>();
        var userEntityStore = new MemoryUserEntityStore(memoryStore);
        var userList = CreateDummyUsers();

        // Save users to memory store
        memoryStore.SaveMany(userList, u => u.Id);

        // Define filter with a role that doesn't exist
        var filter = new UserFilter { Role = "NonExistentRole" };

        // Act
        var exists = await userEntityStore.AnyAsync(filter);

        // Assert
        exists.Should().BeFalse();
    }
    
    /// <summary>
    /// Tests <see cref="MemoryUserEntityStore.CountAsync(UserFilter, CancellationToken)"/>.
    /// </summary>
    [Fact]
    public async Task CountAsync_WithFilter_ReturnsCorrectCount()
    {
        // Arrange
        var memoryStore = new MemoryStore<User>();
        var userEntityStore = new MemoryUserEntityStore(memoryStore);
        var userList = CreateDummyUsers();
        memoryStore.SaveMany(userList, u => u.Id);

        var filter = new UserFilter { Roles = new List<string> { "Patient", "Doctor" } };

        // Act
        var result = await userEntityStore.CountAsync(filter);

        // Assert
        result.Should().Be(userList.Count(u => filter.Roles.Contains(u.Role)));
    }
    
    private static List<User> CreateDummyUsers()
    {
        return
            [
                new User
                    {
                        Auth0Id = Guid.NewGuid().ToString(),
                        Id = Guid.NewGuid().ToString(),
                        Role = "Patient"
                    },

                new User
                    {
                        Auth0Id = Guid.NewGuid().ToString(),
                        Id = Guid.NewGuid().ToString(),
                        Role = "Doctor"
                    },

                new User
                    {
                        Auth0Id = Guid.NewGuid().ToString(),
                        Id = Guid.NewGuid().ToString(),
                        Role = "Admin"
                    }
            ];
    }
}