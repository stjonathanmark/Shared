namespace Shared.Security;

public abstract class BaseUserProfile<TId, TUserId> : BaseEntity<TId>
    where TId : struct
    where TUserId : IEquatable<TUserId>
{
    public TUserId UserId { get; set; } = default!;
}
