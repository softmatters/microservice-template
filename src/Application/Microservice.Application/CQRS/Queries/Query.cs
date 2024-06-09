using MediatR;
using Microservice.Application.DTOS.Responses;

namespace Microservice.Application.CQRS.Queries;

public class Query(int id) : IRequest<QueryResponse>
{
    public int Id { get; } = id;
}