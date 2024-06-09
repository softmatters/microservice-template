using MediatR;
using MicroserviceTemplate.Application.CQRS.Commands;
using MicroserviceTemplate.Application.CQRS.Queries;
using MicroserviceTemplate.Application.DTOS.Requests;
using MicroserviceTemplate.Application.DTOS.Responses;
using MicroserviceTemplate.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTemplate.WebApi.Controllers;

// TODO: Example API Controller using CQRS pattern, update as needed
[ApiController]
[Route("[controller]")]
public class ExampleController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Produces<Entity>]
    public async Task<QueryResponse> QueryRequest(int id)
    {
        var query = new Query(id);

        return await mediator.Send(query);
    }

    [HttpPost]
    public async Task<CommandResponse> CommandRequest(CommandRequest commandRequest)
    {
        var request = new Command(commandRequest);

        return await mediator.Send(request);
    }
}