﻿using MediatR;
using MicroserviceTemplate.Application.DTOS.Requests;
using MicroserviceTemplate.Application.DTOS.Responses;

namespace MicroserviceTemplate.Application.CQRS.Commands;

// TODO: This is an example command for CQRS pattern
// Rename as appropriate
public class Command(CommandRequest request) : IRequest<CommandResponse>
{
    public CommandRequest Request { get; set; } = request;
}