using System.Security.Cryptography;

namespace Shared.Security.Encryption;

public interface IRsaService
{
    RSA LoadRsaKey(string path);
}
