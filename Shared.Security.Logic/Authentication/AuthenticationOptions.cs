namespace Shared.Security.Authentication;

public class AuthenticationOptions
{
    public const string SectionName = "Authentication";
    
    public string DefaultRole { get; set; } = "User";

    public bool RequireEmailConfirmation { get; set; } = true;

    public string EmailConfirmationTemplateName { get; set; } = "confirm-email";

    public string EmailConfirmationUrl { get; set; } = "https://localhost/confirm-email?userId={0}&token={1}";

    public string RecoverPasswordTemplateName { get; set; } = "recover-password";

    public string RecoverPasswordUrl { get; set; } = "https://localhost/recover-password?userId={0}&token={1}";
}


