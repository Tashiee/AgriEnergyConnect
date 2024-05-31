using AgriEnergyConnect.Domain.Products;

namespace AgriEnergyConnect.Application.Farmer;

public sealed class ViewProductsAsFarmerResponse
{
    public  IReadOnlyList<Product> Products { get; set; } = new List<Product>();
}