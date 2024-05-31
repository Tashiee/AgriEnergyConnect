using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Webapp.Components.Pages.User.Registration;

public sealed class RegistrationRequest
{
    [Required] public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required] public string Email { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100, ErrorMessage = "The Password must be at least 8 characters", MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    public bool TAndCAccepted { get; set; }
} 