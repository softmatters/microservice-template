using Mapster;
using MicroserviceTemplate.Application.Contracts.Repositories;
using MicroserviceTemplate.Application.Contracts.Services;
using MicroserviceTemplate.Application.DTOS.Requests;
using MicroserviceTemplate.Application.DTOS.Responses;
using MicroserviceTemplate.Application.Specifications;
using MicroserviceTemplate.Domain.Entities;

namespace MicroserviceTemplate.Services;

// TODO: This is the example implementation of a service calling
// repository methods and executing commands like create, update, get
// please update as necessary
public class Service(IRepository repository) : IService
{
    // usually the create command to create a record in the database
    public async Task<CommandResponse> ExecuteCreateCommand(CommandRequest request)
    {
        var entity = request.Adapt<Entity>();

        var entityFromDb = await repository.Create(entity);

        return entityFromDb.Adapt<CommandResponse>();
    }

    // usually the update command to update the record in the database
    public async Task<CommandResponse> ExcecuteUpdateCommand(int id, CommandRequest request)
    {
        var entity = request.Adapt<Entity>();

        var entityFromDb = await repository.Update(id, entity);

        return entityFromDb.Adapt<CommandResponse>();
    }

    // usually the get command to get the record from the database
    public async Task<QueryResponse> ExecuteQuery(int id)
    {
        var specification = new GetSpecification(id);

        var entityFromDb = await repository.Get(specification);

        return entityFromDb.Adapt<QueryResponse>();
    }
}