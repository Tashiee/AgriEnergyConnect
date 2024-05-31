using AgriEnergyConnect.Application.Abstractions.Authentication;
using AgriEnergyConnect.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace AgriEnergyConnect.Infrastructure.Authentication;

public sealed class AuthenticationService(
    UserManager<ApplicationIdentityUser> userManager,
    SignInManager<ApplicationIdentityUser> signInManager)
    : IAuthenticationService
{
    public async Task<string?> RegisterAsync(User user, string password, CancellationToken cancellationToken)
    {
        var applicationUser = new ApplicationIdentityUser
        {
            UserName = user.Email.Value,
            Email = user.Email.Value,
            FirstName = user.FirstName.Value, 
            GivenName = user.FirstName.Value,
            LastName = user.LastName.Value,
            FullName = $"{user.FirstName.Value} {user.LastName.Value}"
        };

        var result = await userManager.CreateAsync(applicationUser, password);
        return result.Succeeded ? applicationUser.Id : null;
    }

    public async Task<string?> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return null;
        }

        var result = await signInManager.PasswordSignInAsync(user, password, false, false);
        return !result.Succeeded ? null : user.Id;
    }
  
    public async Task SignOutAsync()
    {
        await signInManager.SignOutAsync();
    }
}