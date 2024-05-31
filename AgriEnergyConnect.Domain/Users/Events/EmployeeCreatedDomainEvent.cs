using AgriEnergyConnect.Domain.Abstractions;

namespace AgriEnergyConnect.Domain.Users.Events;

public sealed record EmployeeCreatedDomainEvent(Guid Id): IDomainEvent;