using System.Security.Cryptography;

namespace Shared.Security.Encryption;

public class RsaService : IRsaService
{
    public RSA LoadRsaKey(string path)
    {
        var rsa = RSA.Create();

        if (!File.Exists(path))
            throw new FileNotFoundException("RSA key file not found", path);

        var pemContent = File.ReadAllText(path);
        rsa.ImportFromPem(pemContent.ToCharArray());

        return rsa;
    }
}
