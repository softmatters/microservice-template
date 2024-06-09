using MediatR;
using Microservice.Application.DTOS.Requests;
using Microservice.Application.DTOS.Responses;

namespace Microservice.Application.CQRS.Commands;

// TODO: This is an example command for CQRS pattern
// Rename as appropriate
public class Command(CommandRequest request) : IRequest<CommandResponse>
{
    public CommandRequest Request { get; set; } = request;
}