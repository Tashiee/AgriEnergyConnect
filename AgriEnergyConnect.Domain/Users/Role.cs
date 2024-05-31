namespace AgriEnergyConnect.Domain.Users;

public sealed class Role(int id, string name)
{
    public ICollection<User> Users { get; init; } = new List<User>();

    public int Id { get; init; } = id;
    
    public string Name { get; init; } = name;
    
    public static readonly Role Employee = new(1, "Employee");
    
    public static readonly Role Farmer= new(2, "Farmer");
    
}
