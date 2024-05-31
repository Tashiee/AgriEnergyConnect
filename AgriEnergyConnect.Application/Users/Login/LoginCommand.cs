using AgriEnergyConnect.Application.Abstractions.Messaging;

namespace AgriEnergyConnect.Application.Users.Login;

public sealed record LoginCommand(string username, string password) : ICommand<bool>; 