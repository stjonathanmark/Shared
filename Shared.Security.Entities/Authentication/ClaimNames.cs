using System.Security.Claims;

namespace Shared.Security.Authentication;

public class ClaimNames
{
    public const string Subject = ClaimTypes.NameIdentifier;
    public const string Name = "name";
    public const string UserId = "uid";
    public const string Username = "usr";
    public const string Role = "rle";
    public const string Roles = "rls";
    public const string Email = "email";
    public const string DonorId = "did";
}
