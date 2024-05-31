using AgriEnergyConnect.Domain.Abstractions;
using MediatR;

namespace AgriEnergyConnect.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
} 