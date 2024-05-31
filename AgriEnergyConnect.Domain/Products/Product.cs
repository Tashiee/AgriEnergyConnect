using AgriEnergyConnect.Domain.Abstractions;
using AgriEnergyConnect.Domain.Products.Events;
using AgriEnergyConnect.Domain.Shared;
using AgriEnergyConnect.Domain.Users;

namespace AgriEnergyConnect.Domain.Products;

public sealed class Product : Entity
{
    public ProductName Name { get; private set; }

    public Category Category { get; private set; }

    public DateTime ProductionDateUtc { get; set; }

    public Photo? Photo { get; set; }

    public Guid FarmerId { get; set; }

    public User Farmer { get; set; }
    
    private Product(Guid id, ProductName name, Category category, DateTime productionDateUtc, Photo photo, Guid farmerId)
        : base(id)
    {
        Name = name;
        Category = category;
        ProductionDateUtc = productionDateUtc;
        Photo = photo;
        FarmerId = farmerId;
    }
    
    private Product()
    {
    }
    
    public static Product CreateProduct(ProductName name, Category category, DateTime productionDateUtc, Photo photo, Guid farmerId)
    {
   
        var product = new Product(Guid.NewGuid(), name, category, productionDateUtc, photo, farmerId);
     
        product.RaiseDomainEvent(new ProductCreatedEvent(product.Id));
        
        return product;
    }
}