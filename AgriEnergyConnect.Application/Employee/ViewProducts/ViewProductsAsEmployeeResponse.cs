namespace AgriEnergyConnect.Application.Employee.ViewProducts;

public sealed class ViewProductsAsEmployeeResponse
{
    public  IReadOnlyList<ProductsEmployeeModel> Products { get; set; } = new List<ProductsEmployeeModel>();
}