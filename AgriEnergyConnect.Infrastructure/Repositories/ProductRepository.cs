using AgriEnergyConnect.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Infrastructure.Repositories;

internal sealed class ProductRepository(ApplicationDbContext dbContext) : Repository<Product>(dbContext), IProductRepository
{
    public async Task<IReadOnlyCollection<Product>> GetProductsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Set<Product>().Include(product=> product.Farmer).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<Product>> GetProductsForLoggedInFarmerAsync(Guid farmerId, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Product>().Where(product => product.FarmerId == farmerId).ToListAsync(cancellationToken: cancellationToken);
    }
    
    public override void Add(Product product)
    {
        dbContext.Add(product);
    }

 
}