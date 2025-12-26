namespace Shared.Mailing;

public class SmtpOptions : EmailOptions
{
    public string Host { get; set; } = string.Empty;

    public int Port { get; set; } = 25;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool UseSsl { get; set; } = true;
}
