using AgriEnergyConnect.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Infrastructure;

public class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
    : IdentityDbContext<ApplicationIdentityUser>(options); 