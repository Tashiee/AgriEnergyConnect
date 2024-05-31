using AgriEnergyConnect.Domain.Abstractions;
using AgriEnergyConnect.Domain.Products;
using AgriEnergyConnect.Domain.Shared;
using MediatR;

namespace AgriEnergyConnect.Application.Farmer.AddProduct;

public sealed class CreateProductsCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProductsCommand, string>
{
    public async Task<string> Handle(CreateProductsCommand request, CancellationToken cancellationToken)
    {
        var newProduct = Product.CreateProduct(
            new ProductName(request.name),
            new Category(request.category),
            request.productionDate,
            new Photo( request.imageData), 
            Guid.Parse(request.farmerId));
         
        productRepository.Add(newProduct);
            
        await unitOfWork.SaveChangesAsync(cancellationToken);
            
        return newProduct.Id.ToString();
    }
} 
