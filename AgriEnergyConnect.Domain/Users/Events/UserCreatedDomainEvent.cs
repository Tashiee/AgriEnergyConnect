using AgriEnergyConnect.Domain.Abstractions;

namespace AgriEnergyConnect.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid userId) : IDomainEvent;