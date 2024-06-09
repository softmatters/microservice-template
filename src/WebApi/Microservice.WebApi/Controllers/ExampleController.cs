using MediatR;
using Microservice.Application.CQRS.Commands;
using Microservice.Application.CQRS.Queries;
using Microservice.Application.DTOS.Requests;
using Microservice.Application.DTOS.Responses;
using Microservice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.WebApi.Controllers;

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