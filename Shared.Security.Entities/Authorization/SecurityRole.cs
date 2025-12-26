using Microsoft.AspNetCore.Identity;

namespace Shared.Security.Authorization;

public class SecurityRole<TKey> : IdentityRole<TKey>
    where TKey : struct, IEquatable<TKey>
{ }
