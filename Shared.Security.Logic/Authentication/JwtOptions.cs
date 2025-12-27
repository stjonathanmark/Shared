namespace Shared.Security.Authentication;

public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;

    public IList<string> Audiences { get; set; } = [];

    public int DurationInSeconds = 86400;

    public JwtAlgorithm Algorithm { get; set; }

    public string SecretKey { get; set; } = "InTheNameOfJesus";

    public string PrivateKeyPath { get; set; } = "";

    public string PublicKeyPath { get; set; } = "";
}
