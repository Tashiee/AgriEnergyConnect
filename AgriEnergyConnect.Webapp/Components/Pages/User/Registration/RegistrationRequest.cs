using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Webapp.Components.Pages.User.Registration;

// Define the RegistrationRequest class.
public sealed class RegistrationRequest
{
    // Define the FirstName property with required validation.
    // Set a default value of an empty string if not provided.
    [Required]
    public string FirstName { get; set; } = string.Empty;

    // Define the LastName property with required validation.
    // Set a default value of an empty string if not provided.
    [Required]
    public string LastName { get; set; } = string.Empty;

    // Define the Email property with required validation.
    // Set a default value of an empty string if not provided.
    [Required]
    public string Email { get; set; } = string.Empty;

    // Define the Password property with required validation and length constraint.
    // Set a default value of an empty string if not provided.
    [Required]
    [StringLength(100, ErrorMessage = "The Password must be at least 8 characters", MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    // Define the TAndCAccepted property.
    public bool TAndCAccepted { get; set; }
}
