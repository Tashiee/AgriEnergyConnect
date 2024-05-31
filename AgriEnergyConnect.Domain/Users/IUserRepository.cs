namespace AgriEnergyConnect.Domain.Users;

// Define the IUserRepository interface.
public interface IUserRepository
{
    // Define a method to asynchronously retrieve a user by their ID.
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    // Define a method to asynchronously retrieve all farmers.
    Task<List<User>> GetAllFarmersAsync(CancellationToken cancellationToken = default);

    // Define a method to add a user to the repository.
    void Add(User user);
}
