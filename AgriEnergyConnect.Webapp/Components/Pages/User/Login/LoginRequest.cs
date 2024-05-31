using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Webapp.Components.Pages.User.Login;

public sealed class LoginRequest
{
    //Define the Email property with required validation.
        [Required]
    public string Email { get; set; }

    //Define the Password property with required validation.
    [Required]
    public string Password { get; set; }

    //Define the Remeber me property with required validation.
        [Required]
    public bool RememberMe { get; set; }
}