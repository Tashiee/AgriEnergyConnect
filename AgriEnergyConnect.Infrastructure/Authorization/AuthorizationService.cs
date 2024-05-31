using AgriEnergyConnect.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Infrastructure.Authorization;

internal sealed class AuthorizationService(ApplicationDbContext dbContext)
{
    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
 
        var roles = await dbContext.Set<User>()
            .Where(u => u.IdentityId == identityId)
            .Select(u => new UserRolesResponse
            {
                UserId = u.Id,
                FirstName = u.FirstName.Value.ToString(),
                LastName = u.LastName.Value.ToString(),
                Roles = new List<Role>()
                {
                    new Role(u.RoleId, u.Role.Name)
                }
            })
            .FirstAsync();
 
        
        return roles;
    }
}