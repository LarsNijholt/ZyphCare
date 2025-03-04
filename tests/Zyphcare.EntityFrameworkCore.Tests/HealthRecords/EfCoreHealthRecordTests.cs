using ZyphCare.EntityFramework.Units.HealthRecords;
using ZyphCare.HealthRecords.Entities;
using ZyphCare.HealthRecords.Filters;
using ZyphCare.HealthRecords.Models;

namespace Zyphcare.EntityFrameworkCore.Tests.HealthRecords;

/// <summary>
/// Defines a test class for verifying the functionality and behavior of
/// the Entity Framework Core-based health record store implementation.
/// </summary>
public sealed class EfCoreHealthRecordTests : IDisposable
{
    private readonly SqliteConnection _connection;

    /// <summary>
    /// Contains unit tests for the Entity Framework Core-based health record store
    /// to ensure its methods behave as expected under various conditions.
    /// </summary>
    public EfCoreHealthRecordTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
    }

    /// <summary>
    /// Tests whether the FindAsync method of the Entity Framework Core-based health record store
    /// correctly retrieves a valid health record when provided with an appropriate filter.
    /// </summary>
    [Fact]
    public async Task FindAsync_WithCorrectFilter_ReturnsValidHealthRecord()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var healthRecordStore = await CreateStoreAsync(cancellationToken);
        var healthRecord = new HealthRecord
            {
                Id = Guid.NewGuid().ToString(),
                Type = HealthRecordType.LabResult,
                FileName = "test-lab-result",
                CreatedDate = DateTimeOffset.Now,
                PatientId = Guid.NewGuid().ToString()
            };
        var filter = new HealthRecordFilter { Id = healthRecord.Id };
        await healthRecordStore.SaveAsync(healthRecord, cancellationToken);

        // Act
        var foundHealthRecord = await healthRecordStore.FindAsync(filter, cancellationToken);

        // Assert
        foundHealthRecord.Should().BeEquivalentTo(healthRecord);
    }

    /// <summary>
    /// Tests that the FindManyAsync method correctly applies a given HealthRecordFilter
    /// and returns a collection of health records that match the filter criteria.
    /// Verifies that no invalid or unrelated records are included in the result.
    /// </summary>
    [Fact]
    public async Task FindManyAsync_WithFilter_ReturnsHealthRecords()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var healthRecordStore = await CreateStoreAsync(cancellationToken);
        var healthRecordList = CreateDummyHealthRecords();
        var filter = new HealthRecordFilter { PatientId = healthRecordList.First().PatientId };
        await healthRecordStore.SaveManyAsync(healthRecordList, cancellationToken);

        // Act
        var foundHealthRecords = await healthRecordStore.FindManyAsync(filter, cancellationToken).ToList();

        // Assert
        foundHealthRecords.Should().NotBeNull();
        foundHealthRecords.Should().HaveCount(1);
        foundHealthRecords.Should().OnlyContain(hr => hr.PatientId == filter.PatientId);
    }

    /// <summary>
    /// Ensures that a valid health record is correctly saved to the health record store
    /// and can be retrieved successfully with matching properties.
    /// </summary>
    [Fact]
    public async Task SaveAsync_WithValidHealthRecord_SavesRecordSuccessfully()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var healthRecordStore = await CreateStoreAsync(cancellationToken);
        var healthRecord = new HealthRecord
            {
                Id = Guid.NewGuid().ToString(),
                Type = HealthRecordType.LabResult,
                FileName = "test-lab-result",
                CreatedDate = DateTimeOffset.Now,
                PatientId = Guid.NewGuid().ToString()
            };

        // Act
        await healthRecordStore.SaveAsync(healthRecord, cancellationToken);

        // Assert
        var savedHealthRecord = await healthRecordStore.FindAsync(new HealthRecordFilter { Id = healthRecord.Id }, cancellationToken);
        savedHealthRecord.Should().BeEquivalentTo(healthRecord);
    }

    /// <summary>
    /// Ensures that when a health record already exists in the store,
    /// it can be successfully deleted and verified as removed from the store.
    /// </summary>
    [Fact]
    public async Task DeleteAsync_WithExistingHealthRecord_DeletesRecord()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var healthRecordStore = await CreateStoreAsync(cancellationToken);
        var healthRecord = new HealthRecord
            {
                Id = Guid.NewGuid().ToString(),
                Type = HealthRecordType.LabResult,
                FileName = "test-lab-result",
                CreatedDate = DateTimeOffset.Now,
                PatientId = Guid.NewGuid().ToString()
            };
        await healthRecordStore.SaveAsync(healthRecord, cancellationToken);

        // Act
        var result = await healthRecordStore.DeleteAsync(healthRecord, cancellationToken);

        // Assert
        result.Should().BeTrue();
        var deletedHealthRecord = await healthRecordStore.FindAsync(new HealthRecordFilter { Id = healthRecord.Id }, cancellationToken);
        deletedHealthRecord.Should().BeNull();
    }

    /// <summary>
    /// Verifies that the CountAsync method correctly calculates and returns the number of health records
    /// matching the specified filter criteria.
    /// </summary>
    [Fact]
    public async Task CountAsync_WithFilter_ReturnsCorrectCount()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var healthRecordStore = await CreateStoreAsync(cancellationToken);
        var healthRecordList = CreateDummyHealthRecords();
        var filter = new HealthRecordFilter { Type = HealthRecordType.LabResult };
        await healthRecordStore.SaveManyAsync(healthRecordList, cancellationToken);

        // Act
        var result = await healthRecordStore.CountAsync(filter, cancellationToken);

        // Assert
        result.Should().Be(1);
    }

    private static List<HealthRecord> CreateDummyHealthRecords()
    {
        return
            [
                new HealthRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        Type = HealthRecordType.LabResult,
                        FileName = "test-lab-result",
                        CreatedDate = DateTimeOffset.Now,
                        Description = "test-description",
                        PatientId = Guid.NewGuid().ToString()
                    },

                new HealthRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        Type = HealthRecordType.Consultation,
                        FileName = "test-consult-1",
                        Description = "test-description",
                        CreatedDate = DateTimeOffset.Now,
                        PatientId = Guid.NewGuid().ToString()
                    },

                new HealthRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        Type = HealthRecordType.Consultation,
                        FileName = "test-consult-2",
                        CreatedDate = DateTimeOffset.Now,
                        Description = "test-description",
                        PatientId = Guid.NewGuid().ToString()
                    }
            ];
    }

    /// <summary>
    /// Tests that the FindAsync method retrieves a health record from the store
    /// when provided with a valid filter and order, ensuring the returned record
    /// matches the expected criteria and order.
    /// </summary>
    [Fact]
    public async Task FindAsync_WithCorrectFilterAndOrder_ReturnsValidHealthRecord()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var healthRecordStore = await CreateStoreAsync(cancellationToken);
        var healthRecordList = CreateDummyHealthRecords();
        var order = new HealthRecordOrder<string>(hr => hr.FileName!, OrderDirection.Ascending);
        await healthRecordStore.SaveManyAsync(healthRecordList, cancellationToken);

        // Act
        var foundHealthRecord = await healthRecordStore.FindAsync(new HealthRecordFilter { Type = healthRecordList[1].Type }, order, cancellationToken);

        // Assert
        foundHealthRecord.Should().NotBeNull();
        foundHealthRecord.Should().BeEquivalentTo(healthRecordList.OrderBy(r => r.FileName).First());
    }

    /// <summary>
    /// Tests the FindManyAsync method with a specified filter and pagination arguments
    /// to ensure it returns the correct paginated set of health records matching the filter criteria.
    /// </summary>
    [Fact]
    public async Task FindManyAsync_WithFilterAndPageArgs_ReturnsPaginatedHealthRecords()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var healthRecordStore = await CreateStoreAsync(cancellationToken);
        var healthRecordList = CreateDummyHealthRecords();
        await healthRecordStore.SaveManyAsync(healthRecordList, cancellationToken);
        var pageArgs = new PageArgs { Offset = 1, Limit = 1 };
        var filter = new HealthRecordFilter { Type = HealthRecordType.Consultation};

        // Act
        var pageResult = await healthRecordStore.FindManyAsync(filter, pageArgs, cancellationToken);

        // Assert
        pageResult.Should().NotBeNull();
        pageResult.Items.Should().HaveCount(pageArgs.Limit.Value);
        pageResult.Items.Should().OnlyContain(hr => hr.Type == HealthRecordType.Consultation);
        pageResult.TotalCount.Should().Be(2);
    }

    /// <summary>
    /// Validates that the method retrieves health records that match the specified filter criteria,
    /// orders them according to the provided ordering definition,
    /// and returns the records in a paginated format with the correct count and order validation.
    /// </summary>
    [Fact]
    public async Task FindManyAsync_WithFilterOrderingAndPagination_ReturnsOrderedPaginatedHealthRecords()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var healthRecordStore = await CreateStoreAsync(cancellationToken);
        var healthRecordList = CreateDummyHealthRecords();
        await healthRecordStore.SaveManyAsync(healthRecordList, cancellationToken);
        var order = new HealthRecordOrder<DateTimeOffset>(hr => hr.CreatedDate, OrderDirection.Descending);
        var pageArgs = new PageArgs { Offset = 0, Limit = 2 };

        // Act
        var pageResult = await healthRecordStore.FindManyAsync(
        new HealthRecordFilter { Type = HealthRecordType.Consultation },
        order,
        pageArgs,
        cancellationToken);

        // Assert
        pageResult.Should().NotBeNull();
        pageResult.Items.Should().HaveCount(pageArgs.Limit.Value);
        pageResult.Items.Should().BeInDescendingOrder(x => x.CreatedDate);
        pageResult.TotalCount.Should().Be(2);
    }

    /// <summary>
    /// Ensures that the FindManyAsync method of the EF Core-based health record store
    /// returns a list of health records filtered by the specified filter criteria and
    /// ordered as defined in the provided order configuration.
    /// </summary>
    [Fact]
    public async Task FindManyAsync_WithFilterAndOrder_ReturnsOrderedHealthRecords()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var healthRecordStore = await CreateStoreAsync(cancellationToken);
        var healthRecordList = CreateDummyHealthRecords();
        var order = new HealthRecordOrder<HealthRecordType>(hr => hr.Type, OrderDirection.Ascending);
        await healthRecordStore.SaveManyAsync(healthRecordList, cancellationToken);

        // Act
        var foundHealthRecords = await healthRecordStore.FindManyAsync(new HealthRecordFilter { FileName = "test-consult-1" }, order, cancellationToken);

        // Assert
        var healthRecords = foundHealthRecords.ToList();
        healthRecords.Should().NotBeNull();
        healthRecords.Should().BeInAscendingOrder(hr => hr.Type);
        healthRecords.Should().OnlyContain(hr => hr.FileName == "test-consult-1");
    }

    /// <summary>
    /// Verifies that the method returns true when the supplied filter matches at least one existing health record.
    /// </summary>
    [Fact]
    public async Task AnyAsync_WithExistingFilter_ReturnsTrue()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var healthRecordStore = await CreateStoreAsync(cancellationToken);
        var healthRecord = new HealthRecord
            {
                Id = Guid.NewGuid().ToString(),
                PatientId = "patient-3",
                Type = HealthRecordType.Invoice,
                Description = "Flu shot",
                CreatedDate = DateTimeOffset.Now
            };
        await healthRecordStore.SaveAsync(healthRecord, cancellationToken);

        // Act
        var result = await healthRecordStore.AnyAsync(new HealthRecordFilter { PatientId = "patient-3" }, cancellationToken);

        // Assert
        result.Should().BeTrue();
    }

    private async Task<EfCoreHealthRecordStore> CreateStoreAsync(CancellationToken cancellationToken)
    {
        var options = new DbContextOptionsBuilder<HealthRecordZyphCareDbContext>()
            .UseSqlite(_connection).Options;

        await using var context = new HealthRecordZyphCareDbContext(options);

        await context.Database.EnsureCreatedAsync(cancellationToken);
        var dbContextFactory = A.Fake<IDbContextFactory<HealthRecordZyphCareDbContext>>();
        var dbExceptionHandler = A.Fake<IDbExceptionHandler<HealthRecordZyphCareDbContext>>();

        A.CallTo(() => dbContextFactory.CreateDbContextAsync(A<CancellationToken>._))
            .ReturnsLazily(() => new HealthRecordZyphCareDbContext(options));

        var entityStore = new EntityStore<HealthRecordZyphCareDbContext, HealthRecord>(dbContextFactory, dbExceptionHandler);
        return new EfCoreHealthRecordStore(entityStore);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _connection.Dispose();
    }
}