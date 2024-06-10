﻿using MediatR;
using MicroserviceTemplate.Application.Contracts.Services;
using MicroserviceTemplate.Application.CQRS.Queries;
using MicroserviceTemplate.Application.DTOS.Responses;
using Microsoft.Extensions.Logging;

namespace MicroserviceTemplate.Application.CQRS.Handlers.QueryHandlers;

// TODO: This is just an example of a query handler which takes logger and IService
// as dependencies, please rename/modify as appropriate
public class QueryHandler(ILogger<QueryHandler> logger, IService service) : IRequestHandler<Query, QueryResponse>
{
    public async Task<QueryResponse> Handle(Query request, CancellationToken cancellationToken)
    {
        // TODO: use high performance logging using LoggerMessage
        // if you have package referenced that supports high performance logging
        // replace with those methods.
        logger.LogInformation("Executing query with ID = {Id}", request.Id);

        return await service.ExecuteQuery(request.Id);
    }
}