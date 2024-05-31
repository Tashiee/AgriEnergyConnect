using AgriEnergyConnect.Application.Abstractions.Messaging;

namespace AgriEnergyConnect.Application.Users.Registration;

public sealed record EmployeeRegistrationCommand(string firstName, string lastName, string email, string password) :  ICommand<string>;