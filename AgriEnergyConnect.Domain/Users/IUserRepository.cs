namespace AgriEnergyConnect.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
     
    Task<List<User>> GetAllFarmersAsync(CancellationToken cancellationToken = default);
    void Add(User user);
} 