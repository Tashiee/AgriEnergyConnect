using AgriEnergyConnect.Application.Abstractions.Email;

namespace AgriEnergyConnect.Infrastructure.EmailService;

public sealed class EmailService: IEmailService
{
    public Task SendAsync(Domain.Users.Email recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}