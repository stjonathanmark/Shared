using Shared.Location;

namespace Shared.Security;

public class SecurityUserAddress<TUserId> : Address
    where TUserId : struct, IEquatable<TUserId>
{
    public TUserId UserId { get; set; }
}
