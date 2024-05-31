using AgriEnergyConnect.Application.Abstractions.Messaging;

namespace AgriEnergyConnect.Application.Farmer;

public sealed record ViewProductsAsFarmerQuery(Guid farmerId): IQuery<ViewProductsAsFarmerResponse>;