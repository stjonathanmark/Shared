using Microsoft.AspNetCore.Identity;

namespace Shared.Security;

public abstract class BaseUser<TKey> : IdentityUser<TKey>
    where TKey : IEquatable<TKey>
{ }
