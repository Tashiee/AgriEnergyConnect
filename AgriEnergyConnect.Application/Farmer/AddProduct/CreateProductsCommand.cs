using MediatR;

namespace AgriEnergyConnect.Application.Farmer.AddProduct;

public record CreateProductsCommand(string name, string category, DateTime productionDate, string imageData, string farmerId)
    : IRequest<string>;
