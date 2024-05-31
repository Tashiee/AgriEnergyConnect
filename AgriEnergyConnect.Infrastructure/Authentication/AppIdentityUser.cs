using Microsoft.AspNetCore.Identity;

namespace AgriEnergyConnect.Infrastructure.Authentication;

public class ApplicationIdentityUser: IdentityUser
{
    public string FirstName { get; set; }  
    
    public string LastName { get; set; }
    
    public string GivenName { get; set; }
    
    public string FullName { get; set; }
}  