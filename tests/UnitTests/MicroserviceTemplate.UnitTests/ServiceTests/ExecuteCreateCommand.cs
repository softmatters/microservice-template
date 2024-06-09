using AutoFixture.Xunit2;
using MicroserviceTemplate.Application.Contracts.Repositories;
using MicroserviceTemplate.Application.DTOS.Requests;
using MicroserviceTemplate.Application.DTOS.Responses;
using MicroserviceTemplate.Infrastructure.Repositories;
using MicroserviceTemplate.Services;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace MicroserviceTemplate.UnitTests.ServiceTests;

/// <summary>
/// Covers the tests for ExecuteCommand method
/// </summary>
public class ExecuteCreateCommand : TestServiceBase<Service>
{
    private readonly Infrastructure.DbContext _context;

    private readonly Repository _repository;

    public ExecuteCreateCommand()
    {
        var options = new DbContextOptionsBuilder<Infrastructure.DbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString("N")).Options;

        _context = new Infrastructure.DbContext(options);
        _repository = new Repository(_context);
    }

    /// <summary>
    /// Tests that entity is created
    /// </summary>
    /// <param name="commandRequest">Represents the model for new entity request</param>
    [Theory, AutoData]
    public async Task Returns_CommandResponse(CommandRequest commandRequest)
    {
        // Arrange
        Mocker.Use<IRepository>(_repository);

        Sut = Mocker.CreateInstance<Service>();

        // Act
        var response = await Sut.ExecuteCreateCommand(commandRequest);

        // Assert
        response.ShouldNotBeNull();
        response.ShouldBeOfType<CommandResponse>();
        (await _context.Entities.CountAsync()).ShouldBe(1);
    }
}