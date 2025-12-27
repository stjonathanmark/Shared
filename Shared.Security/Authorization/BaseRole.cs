using Microsoft.AspNetCore.Identity;

namespace Shared.Security.Authorization;

public abstract class BaseRole<TKey> : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
}
