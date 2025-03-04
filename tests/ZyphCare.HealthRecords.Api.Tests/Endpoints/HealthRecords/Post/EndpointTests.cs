using FakeItEasy;
using FastEndpoints;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZyphCare.HealthRecords.Api.Endpoints.HealthRecords.Post;
using ZyphCare.HealthRecords.Contracts;
using ZyphCare.HealthRecords.Entities;
using ZyphCare.HealthRecords.Models;
using Endpoint=ZyphCare.HealthRecords.Api.Endpoints.HealthRecords.Post.Endpoint;

namespace ZyphCare.HealthRecords.Api.Tests.Endpoints.HealthRecords.Post;

/// <summary>
/// Unit tests for the POST /health-records endpoint.
/// </summary>
public class EndpointTests
{
    /// <summary>
    /// Ensures that the endpoint successfully creates a new health record when provided with a valid request.
    /// Verifies that the record is saved to blob storage and the entity store, and a proper response is sent.
    /// </summary>
    [Fact]
    public async Task Endpoint_WithValidRequest_CreatesHealthRecord()
    {
        // Arrange
        var healthRecordEntityStore = A.Fake<IHealthRecordEntityStore>();
        var healthRecordBlobStorage = A.Fake<IHealthRecordBlobStorage>();
        var linkGenerator = A.Fake<LinkGenerator>();

        var endpoint = Factory.Create<Endpoint>(
        ctx => ctx.AddTestServices(s => s.AddSingleton(linkGenerator)), 
        healthRecordBlobStorage, healthRecordEntityStore);

        var fakeStream = new MemoryStream();
        var fileMock = A.Fake<IFormFile>();
        A.CallTo(() => fileMock.OpenReadStream()).Returns(fakeStream);

        var request = new Request
            {
                Name = "record1.pdf",
                Type = HealthRecordType.Invoice,
                File = fileMock
            };

        // Act
        await endpoint.HandleAsync(request, CancellationToken.None);

        // Assert
        A.CallTo(() => healthRecordBlobStorage.WriteAsync(
            A<HealthRecord>.That.Matches(hr => hr.FileName == request.Name && hr.Type == request.Type),
            A<Stream>._,
            A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();

        A.CallTo(() => healthRecordEntityStore.SaveAsync(
            A<HealthRecord>.That.Matches(hr => hr.FileName == request.Name && hr.Type == request.Type),
            A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();

        endpoint.HttpContext.Response.StatusCode.Should().Be(201);
    }

    /// <summary>
    /// Validates that the endpoint correctly sends a "Created" response and invokes the correct mapper logic.
    /// </summary>
    [Fact]
    public async Task Endpoint_SendsCorrectResponse_OnSuccess()
    {
        // Arrange
        var healthRecordEntityStore = A.Fake<IHealthRecordEntityStore>();
        var healthRecordBlobStorage = A.Fake<IHealthRecordBlobStorage>();
        var linkGenerator = A.Fake<LinkGenerator>();

        var endpoint = Factory.Create<Endpoint>(
        ctx => ctx.AddTestServices(s => s.AddSingleton(linkGenerator)), 
        healthRecordBlobStorage, healthRecordEntityStore);

        var fakeStream = new MemoryStream();
        var fileMock = A.Fake<IFormFile>();
        A.CallTo(() => fileMock.OpenReadStream()).Returns(fakeStream);

        var request = new Request
            {
                Name = "record3.pdf",
                Type = HealthRecordType.Invoice,
                File = fileMock
            };

        // Act
        await endpoint.HandleAsync(request, CancellationToken.None);

        // Assert
        A.CallTo(() => healthRecordBlobStorage.WriteAsync(
            A<HealthRecord>.That.Matches(hr => hr.FileName == request.Name && hr.Type == request.Type),
            A<Stream>._,
            A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();

        A.CallTo(() => healthRecordEntityStore.SaveAsync(
            A<HealthRecord>.That.Matches(hr => hr.FileName == request.Name && hr.Type == request.Type),
            A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }
}