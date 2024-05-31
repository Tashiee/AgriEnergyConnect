
using AgriEnergyConnect.Domain.Users;
using Microsoft.EntityFrameworkCore;

// Define the namespace for the AuthorizationService class.
namespace AgriEnergyConnect.Infrastructure.Authorization
{
    // Define the AuthorizationService class.
    internal sealed class AuthorizationService
    {
        // Define a constructor that takes an instance of ApplicationDbContext as a parameter.
        public AuthorizationService(ApplicationDbContext dbContext)
        {
            // Assign the provided ApplicationDbContext instance to a local variable.
            _dbContext = dbContext;
        }

        // Define a method to asynchronously retrieve roles for a user.
        public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
        {
            // Use the provided DbContext to query the database.
            // Select the user's roles based on the provided identityId.
            var roles = await _dbContext.Set<User>()
                .Where(u => u.IdentityId == identityId)
                .Select(u => new UserRolesResponse
                {
                    // Populate the properties of the UserRolesResponse object.
                    UserId = u.Id,
                    FirstName = u.FirstName.Value.ToString(),
                    LastName = u.LastName.Value.ToString(),
                    // Create a list of Role objects containing the user's role.
                    Roles = new List<Role>()
                    {
                        new Role(u.RoleId, u.Role.Name)
                    }
                })
                // Retrieve the first result asynchronously.
                .FirstAsync();

            // Return the roles retrieved for the user.
            return roles;
        }

        // Define a private field to store the ApplicationDbContext instance.
        private readonly ApplicationDbContext _dbContext;
    }
}
