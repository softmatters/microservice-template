using MediatR;
using Microservice.Application.Contracts.Services;
using Microservice.Application.CQRS.Commands;
using Microservice.Application.DTOS.Responses;
using Microsoft.Extensions.Logging;

namespace Microservice.Application.CQRS.Handlers.CommandHandlers;

// TODO: This is just an example of a command handler which takes logger and IService
// as dependencies, please rename/modify as appropriate
public class CommandHandler(ILogger<CommandHandler> logger, IService serivce) : IRequestHandler<Command, CommandResponse>
{
    public async Task<CommandResponse> Handle(Command request, CancellationToken cancellationToken)
    {
        // TODO: use high performance logging using LoggerMessage
        // if you have package referenced that supports high performance logging
        // replace with those methods.
        logger.LogInformation("Excecuting Command");

        return await serivce.ExecuteCreateCommand(request.Request);
    }
}