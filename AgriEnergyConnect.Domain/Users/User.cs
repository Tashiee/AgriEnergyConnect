using AgriEnergyConnect.Domain.Abstractions;
using AgriEnergyConnect.Domain.Products;
using AgriEnergyConnect.Domain.Shared;
using AgriEnergyConnect.Domain.Users.Events;

namespace AgriEnergyConnect.Domain.Users;

// Define the User class, inheriting from Entity.
public class User : Entity
{
    // Define properties for user details.
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public Photo? Photo { get; private set; }

    // Define properties for user role.
    public int RoleId { get; set; }
    public Role Role { get; set; }

    // Define a collection of products associated with this user.
    public ICollection<Product> Products { get; private set; } = new List<Product>();

    // Define the IdentityId property to store identity information.
    public string? IdentityId { get; private set; } = string.Empty;

    // Define a private constructor to initialize a User object.
    private User(Guid id, FirstName firstName, LastName lastName, Email email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    // Define a parameterless private constructor.
    private User()
    {
    }

    // Define a factory method to create an employee user.
    public static User EmployeeFactory(FirstName firstName, LastName lastName, Email email)
    {
        // Create a new User object with a generated ID and provided details.
        var employee = new User(Guid.NewGuid(), firstName, lastName, email)
        {
            // Set the RoleId to the ID of the Employee role.
            RoleId = Role.Employee.Id
        };

        // Raise a domain event indicating the creation of an employee.
        employee.RaiseDomainEvent(new EmployeeCreatedDomainEvent(employee.Id));

        // Return the created employee user.
        return employee;
    }

    // Define a factory method to create a farmer user.
    public static User FarmerFactory(FirstName firstName, LastName lastName, Email email)
    {
        // Create a new User object with a generated ID and provided details.
        var farmer = new User(Guid.NewGuid(), firstName, lastName, email)
        {
            // Set the RoleId to the ID of the Farmer role.
            RoleId = Role.Farmer.Id
        };

        // Raise a domain event indicating the creation of a farmer.
        farmer.RaiseDomainEvent(new FarmerCreatedDomainEvent(farmer.Id));

        // Return the created farmer user.
        return farmer;
    }

    // Define a method to set the IdentityId property.
    public void SetIdentityId(string? identityId)
    {
        IdentityId = identityId;
    }
}
