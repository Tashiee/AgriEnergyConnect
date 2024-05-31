namespace AgriEnergyConnect.Domain.Products;

public interface IProductRepository
{
    Task<IReadOnlyCollection<Product>> GetProductsAsync(CancellationToken cancellationToken);
    
    Task<IReadOnlyCollection<Product>> GetProductsForLoggedInFarmerAsync(Guid farmerId, CancellationToken cancellationToken);
    
    void Add(Product product);

    
}