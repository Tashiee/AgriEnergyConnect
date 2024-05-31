using AgriEnergyConnect.Domain.Abstractions;
using MediatR;

namespace AgriEnergyConnect.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
} 