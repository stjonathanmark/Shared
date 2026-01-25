using Shared.Location;

namespace Shared.Security;

public class SecurityUserAddress<TUserId> : Address<TUserId>
    where TUserId : struct, IEquatable<TUserId>
{ }
