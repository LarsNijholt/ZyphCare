using FakeItEasy;
using FastEndpoints;
using ZyphCare.HealthRecords.Api.Endpoints.HealthRecords.Delete;
using ZyphCare.HealthRecords.Contracts;
using ZyphCare.HealthRecords.Entities;
using ZyphCare.HealthRecords.Filters;
using ZyphCare.HealthRecords.Models;

namespace ZyphCare.HealthRecords.Api.Tests.Endpoints.HealthRecords.Delete;

/// <summary>
/// Tests for the Delete <see cref="Endpoint"/>.
/// </summary>
public sealed class EndpointTests
{
    /// <summary>
    /// Validates that the endpoint successfully deletes a health record when provided with a valid request.
    /// Ensures that the health record is retrieved, deleted from the entity store, and its associated blob storage is cleaned up.
    /// </summary>
    [Fact]
    public async Task Endpoint_WithValidRequest_DeletesHealthRecord()
    {
        // Arrange.
        var request = new Request
            {
                Id = Guid.NewGuid().ToString(),
                PatientId = Guid.NewGuid().ToString()
            };
        var healthRecordEntityStore = A.Fake<IHealthRecordEntityStore>();
        var healthRecordBlobStorage = A.Fake<IHealthRecordBlobStorage>();
        var filter = new HealthRecordFilter { Id = request.Id, PatientId = request.PatientId };
        var expectedHealthRecord = new HealthRecord
            {
                Id = request.Id,
                PatientId = request.PatientId,
                CreatedDate = DateTimeOffset.Now,
                Description = "test-description",
                FileName = "test-file-name",
                Type = HealthRecordType.Invoice
            };

        var endpoint = Factory.Create<Endpoint>(healthRecordEntityStore, healthRecordBlobStorage);
        
        A.CallTo(() => healthRecordEntityStore.FindAsync(A<HealthRecordFilter>.That.Matches(x => x.Id == filter.Id && x.PatientId == filter.PatientId), A<CancellationToken>._))
            .Returns(expectedHealthRecord);

        // Act.
        await endpoint.HandleAsync(request, CancellationToken.None);
        
        // Assert.
        A.CallTo(() => healthRecordEntityStore.DeleteAsync(A<HealthRecord>.That.Matches(x => x.Id == filter.Id && x.PatientId == filter.PatientId), A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => healthRecordEntityStore.FindAsync(A<HealthRecordFilter>.That.Matches(x => x.Id == filter.Id && x.PatientId == filter.PatientId), A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => healthRecordBlobStorage.Delete(A<HealthRecord>.That.Matches(x => x.Id == filter.Id && x.PatientId == filter.PatientId)))
            .MustHaveHappenedOnceExactly();
    }

    /// <summary>
    /// Ensures that the endpoint correctly handles an invalid request by not deleting a health record
    /// or associated blob. Validates that the health record is looked up but deletion actions do not occur.
    /// </summary>
    [Fact]
    public async Task Endpoint_WithInvalidRequest_ReturnsMethod()
    {
        // Arrange.
        var request = new Request
            {
                Id = Guid.NewGuid().ToString(),
                PatientId = Guid.NewGuid().ToString()
            };
        var healthRecordEntityStore = A.Fake<IHealthRecordEntityStore>();
        var healthRecordBlobStorage = A.Fake<IHealthRecordBlobStorage>();
        var filter = new HealthRecordFilter { Id = request.Id, PatientId = request.PatientId };
        HealthRecord expectedHealthRecord = null!;

        var endpoint = Factory.Create<Endpoint>(healthRecordEntityStore, healthRecordBlobStorage);
        
        A.CallTo(() => healthRecordEntityStore.FindAsync(A<HealthRecordFilter>.That.Matches(x => x.Id == filter.Id && x.PatientId == filter.PatientId), A<CancellationToken>._))
            .Returns(expectedHealthRecord);

        // Act.
        await endpoint.HandleAsync(request, CancellationToken.None);
        
        // Assert.
        A.CallTo(() => healthRecordEntityStore.DeleteAsync(A<HealthRecord>.That.Matches(x => x.Id == filter.Id && x.PatientId == filter.PatientId), A<CancellationToken>._))
            .MustNotHaveHappened();
        A.CallTo(() => healthRecordEntityStore.FindAsync(A<HealthRecordFilter>.That.Matches(x => x.Id == filter.Id && x.PatientId == filter.PatientId), A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => healthRecordBlobStorage.Delete(A<HealthRecord>.That.Matches(x => x.Id == filter.Id && x.PatientId == filter.PatientId)))
            .MustNotHaveHappened();
    }
}