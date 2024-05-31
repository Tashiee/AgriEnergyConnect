using AgriEnergyConnect.Application.Abstractions.Messaging;
using AgriEnergyConnect.Domain.Abstractions;
using AgriEnergyConnect.Domain.Products;

namespace AgriEnergyConnect.Application.Employee.ViewProducts;

public sealed class ViewProductsAsEmployeeQueryHandler(IProductRepository productRepository)
    : IQueryHandler<ViewProductsAsEmployeeQuery, ViewProductsAsEmployeeResponse>
{
 

    public async Task<Result<ViewProductsAsEmployeeResponse>> Handle(ViewProductsAsEmployeeQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsAsync(cancellationToken);
 
        var response = new ViewProductsAsEmployeeResponse()
        {
            Products = products.ToList().Select(products=> new ProductsEmployeeModel
            {
                FullName = products.Farmer.FirstName.Value + " " + products.Farmer.LastName.Value,
                ProductName = products.Name.Value,
                ProductCategory = products.Category.Value,
                ProductionDate = products.ProductionDateUtc
            }).ToList()
        };

        return response;
    }
}
 