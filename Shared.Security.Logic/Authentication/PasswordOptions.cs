namespace Shared.Security.Authentication;

public class PasswordOptions
{
    public bool RequireUppercase { get; set; } = true;

    public bool RequireLowercase { get; set; } = true;

    public int RequiredLength { get; set; } = 8;

    public bool RequireNonAlphanumeric { get; set; } = true;

    public bool RequireDigit { get; set; } = true;

    public string RecoverPasswordTemplateName { get; set; } = "recover-password";

    public string RecoverPasswordUrl { get; set; } = "https://localhost/recover-password?userId={0}&token={1}";
}
