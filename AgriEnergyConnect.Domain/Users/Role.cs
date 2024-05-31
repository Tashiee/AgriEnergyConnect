namespace AgriEnergyConnect.Domain.Users;

// Define the Role class.
public sealed class Role
{
    // Define a constructor to initialize the Role object.
    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    // Define a collection of users associated with this role.
    public ICollection<User> Users { get; init; } = new List<User>();

    // Define the Id property of the role.
    public int Id { get; init; }

    // Define the Name property of the role.
    public string Name { get; init; }

    // Define static instances of roles.
    public static readonly Role Employee = new Role(1, "Employee");

    public static readonly Role Farmer = new Role(2, "Farmer");
}
