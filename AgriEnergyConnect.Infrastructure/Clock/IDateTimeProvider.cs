using AgriEnergyConnect.Application.Abstractions.Clock;

namespace AgriEnergyConnect.Infrastructure.Clock;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}