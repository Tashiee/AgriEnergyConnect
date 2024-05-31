using AgriEnergyConnect.Domain.Abstractions;

namespace AgriEnergyConnect.Domain.Users.Events;

public sealed record FarmerCreatedDomainEvent(Guid Id) : IDomainEvent;