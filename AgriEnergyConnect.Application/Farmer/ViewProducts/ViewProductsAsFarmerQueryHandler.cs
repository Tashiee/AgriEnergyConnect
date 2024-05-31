using AgriEnergyConnect.Application.Abstractions.Messaging;
using AgriEnergyConnect.Domain.Abstractions;
using AgriEnergyConnect.Domain.Products;

namespace AgriEnergyConnect.Application.Farmer;

public sealed class ViewProductsAsFarmerQueryHandler(IProductRepository productRepository)
    : IQueryHandler<ViewProductsAsFarmerQuery, ViewProductsAsFarmerResponse>
{
    public async Task<Result<ViewProductsAsFarmerResponse>> Handle(ViewProductsAsFarmerQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsForLoggedInFarmerAsync(request.farmerId, cancellationToken);
 
        return new ViewProductsAsFarmerResponse()
        {
            Products = products.ToList()
        };
    }
}