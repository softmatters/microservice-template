using AutoFixture;
using AutoFixture.Xunit2;
using MicroserviceTemplate.Application.Contracts.Repositories;
using MicroserviceTemplate.Application.DTOS.Responses;
using MicroserviceTemplate.Domain.Entities;
using MicroserviceTemplate.Infrastructure.Repositories;
using MicroserviceTemplate.Services;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using static MicroserviceTemplate.UnitTests.TestData;

namespace MicroserviceTemplate.UnitTests.ServiceTests;

/// <summary>
/// Covers the tests for ExecuteQuery method
/// </summary>
public class ExecuteQuery : TestServiceBase<Service>
{
    private readonly Infrastructure.DbContext _context;
    private readonly Repository _repository;

    public ExecuteQuery()
    {
        var options = new DbContextOptionsBuilder<Infrastructure.DbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString("N")).Options;

        _context = new Infrastructure.DbContext(options);
        _repository = new Repository(_context);
    }

    /// <summary>
    /// Tests that correct entity is returned by Id
    /// </summary>
    /// <param name="records">Number of records to seed</param>
    /// <param name="generator">Test Data Generator</param>
    [Theory, InlineAutoData(5)]
    public async Task Returns_Entity_ById(int records, Generator<Entity> generator)
    {
        // Arrange
        Mocker.Use<IRepository>(_repository);

        Sut = Mocker.CreateInstance<Service>();

        // seed data using number of records to seed
        var entities = await SeedData(_context, generator, records);

        // get the random entity id between 0 and 4
        var entityId = entities[Random.Shared.Next(0, 4)].Id;

        // Act
        var response = await Sut.ExecuteQuery(entityId);

        // Assert
        response.ShouldNotBeNull();
        response.ShouldBeOfType<QueryResponse>();
        response.Id.ShouldBe(entityId);
    }

    /// <summary>
    /// Tests that exception is thrown if Id doesn't exist
    /// </summary>
    /// <param name="generator">Test data generator</param>
    [Theory, InlineAutoData(5)]
    public async Task ThrowsException_If_Id_DoesNotExist(int records, Generator<Entity> generator)
    {
        // Arrange
        Mocker.Use<IRepository>(_repository);

        Sut = Mocker.CreateInstance<Service>();

        // seed data using number of records to seed
        var entities = await SeedData(_context, generator, records);

        // get the id that won't exist
        var id = Random.Shared.Next(entities.Max(e => e.Id)) + 1;

        // Act/Assert
        await Should.ThrowAsync<NotImplementedException>(Sut.ExecuteQuery(id));
    }
}