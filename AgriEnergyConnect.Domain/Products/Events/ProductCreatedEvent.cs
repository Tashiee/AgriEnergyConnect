using AgriEnergyConnect.Domain.Abstractions;

namespace AgriEnergyConnect.Domain.Products.Events;

public sealed record ProductCreatedEvent(Guid Id): IDomainEvent;