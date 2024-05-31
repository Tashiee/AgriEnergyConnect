using AgriEnergyConnect.Application.Abstractions.Messaging;

namespace AgriEnergyConnect.Application.Users.Registration;

public sealed record FarmerRegistrationCommand(string firstName, string lastName, string email, string password) :  ICommand<string>;