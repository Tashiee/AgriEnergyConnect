using AgriEnergyConnect.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<User>().Where(x=> x.RoleId == 2).ToListAsync(cancellationToken: cancellationToken);
    }
 
    public async Task<List<User>> GetAllFarmersAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<User>().Where(x=> x.RoleId == 2).ToListAsync(cancellationToken: cancellationToken);
    }

    public override void Add(User user)
    {
        DbContext.Add(user);
    }
}  