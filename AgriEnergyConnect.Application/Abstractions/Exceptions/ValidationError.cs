namespace AgriEnergyConnect.Application.Abstractions.Exceptions;

public sealed record ValidationError(string Field, string Message);