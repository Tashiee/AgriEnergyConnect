using AgriEnergyConnect.Domain.Users;

// Define the namespace for the UserRolesResponse class.
namespace AgriEnergyConnect.Infrastructure.Authorization
{
    // Define the UserRolesResponse class.
    public class UserRolesResponse
    {
        // Define the UserId property.
        public Guid UserId { get; init; }

        // Define the Roles property, initialized as an empty list.
        public List<Role> Roles { get; init; } = [];

        // Define the LastName property, initialized as an empty string.
        public string LastName { get; set; } = string.Empty;

        // Define the FirstName property, initialized as an empty string.
        public string FirstName { get; set; } = string.Empty;
    }
}
