using Microsoft.AspNetCore.Identity;

namespace Shared.Security;

public class SecurityUser<TKey> : IdentityUser<TKey>
    where TKey : struct, IEquatable<TKey>
{ }
