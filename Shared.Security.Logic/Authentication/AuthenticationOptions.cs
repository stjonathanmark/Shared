namespace Shared.Security.Authentication;

public class AuthenticationOptions
{
    public const string SectionName = "Shared:Security:Authentication";
    
    public string DefaultRole { get; set; } = "User";

    public bool RequireEmailConfirmation { get; set; } = true;

    public string EmailConfirmationTemplateName { get; set; } = "confirm-email";

    public string EmailConfirmationUrl { get; set; } = "https://localhost/confirm-email?userId={0}&token={1}";

    public PasswordOptions Password { get; set; } = new();
}


