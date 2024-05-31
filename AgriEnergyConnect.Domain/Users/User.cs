using AgriEnergyConnect.Domain.Abstractions;
using AgriEnergyConnect.Domain.Products;
using AgriEnergyConnect.Domain.Shared;
using AgriEnergyConnect.Domain.Users.Events;

namespace AgriEnergyConnect.Domain.Users;

public class User: Entity
{
    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Email Email { get; private set; }
    
    public Photo? Photo { get; private set; }
 
    public int RoleId { get; set; }
    public Role Role { get; set; }

    public ICollection<Product> Products { get; private set; } = new List<Product>();
 
    public string? IdentityId { get; private set; } = string.Empty;

 
    
    private User(Guid id, FirstName firstName, LastName lastName, Email email)
        : base(id) 
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    private User()
    {
    }

    public static User EmployeeFactory(FirstName firstName, LastName lastName, Email email)
    {
        var employee = new User(Guid.NewGuid(), firstName, lastName, email)
        {
            RoleId = Role.Employee.Id
        };

        employee.RaiseDomainEvent(new EmployeeCreatedDomainEvent(employee.Id));
        
        return employee;
    }
    
    public static User FarmerFactory(FirstName firstName, LastName lastName, Email email)
    {
        var farmer = new User(Guid.NewGuid(), firstName, lastName, email)
        {
            RoleId = Role.Farmer.Id
        };

        farmer.RaiseDomainEvent(new FarmerCreatedDomainEvent(farmer.Id));
        
        return farmer;
    }

    public void SetIdentityId(string? identityId)
    {
        IdentityId = identityId;
    }
}