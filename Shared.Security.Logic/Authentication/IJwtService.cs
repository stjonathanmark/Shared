using System.Security.Claims;

namespace Shared.Security.Authentication;

public interface IJwtService<TUser, TKey>
    where TKey : IEquatable<TKey>
    where TUser : BaseUser<TKey>
{
    event Action<TUser, ClaimsIdentity> AddOptionalUserClaims;

    Task<string> GetTokenAsync(TKey userId, IEnumerable<Claim>? claims = null);

    Task<string> GetTokenAsync(TUser user, IEnumerable<Claim>? claims = null);
}
