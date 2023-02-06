using MediatR;

namespace Gamba.Application.Configuration.Queries;

public interface IQueryHandler<in TQuery, TQueryResult>: IRequestHandler<TQuery, TQueryResult> 
    where TQuery: IRequest<TQueryResult>
{
}