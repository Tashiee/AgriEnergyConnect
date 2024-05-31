
using System.Security.Claims;
using AgriEnergyConnect.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

// Define the namespace for the CustomClaimsTransformer class.
namespace AgriEnergyConnect.Infrastructure.Authorization
{
    // Define the CustomClaimsTransformer class implementing the IClaimsTransformation interface.
    internal sealed class CustomClaimsTransformer : IClaimsTransformation
    {
        // Define a constructor that takes an instance of IServiceProvider as a parameter.
        public CustomClaimsTransformer(IServiceProvider serviceProvider)
        {
            // Assign the provided IServiceProvider instance to a local variable.
            _serviceProvider = serviceProvider;
        }

        // Implement the TransformAsync method required by the IClaimsTransformation interface.
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // Create a new scope for dependency injection.
            using var scope = _serviceProvider.CreateScope();

            // Retrieve an instance of AuthorizationService from the service provider.
            var authorizationService = scope.ServiceProvider.GetRequiredService<AuthorizationService>();

            // Extract the identityId claim from the principal.
            var identityId = principal.GetIdentityId(); // Presumably an extension method.

            // Retrieve the roles associated with the user identity asynchronously.
            var userRoles = await authorizationService.GetRolesForUserAsync(identityId);

            // Create a new ClaimsIdentity to store the transformed claims.
            var claimsIdentity = new ClaimsIdentity();

            // Add claims to the ClaimsIdentity for user information.
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, userRoles.UserId.ToString()));
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.GivenName, userRoles.FirstName));
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.FamilyName, userRoles.LastName));
            claimsIdentity.AddClaim(new Claim("Photo", "user.jpg"));

            // Add claims for each role associated with the user.
            foreach (var role in userRoles.Roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            }

            // Add the ClaimsIdentity to the principal.
            principal.AddIdentity(claimsIdentity);

            // Return the transformed principal.
            return principal;
        }

        // Define a private field to store the IServiceProvider instance.
        private readonly IServiceProvider _serviceProvider;
    }
}
