namespace AgriEnergyConnect.Webapp.Components.Pages.User.Login;

public sealed class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}