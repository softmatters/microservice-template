using MediatR;
using MicroserviceTemplate.Application.DTOS.Responses;

namespace MicroserviceTemplate.Application.CQRS.Queries;

public class Query(int id) : IRequest<QueryResponse>
{
    public int Id { get; } = id;
}