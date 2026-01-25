namespace Shared.Security;

public class SecurityUser<TUserProfile, TProfileId> : BaseUser<string>
    where TUserProfile : BaseUserProfile<TProfileId>
    where TProfileId : struct, IEquatable<TProfileId>
{
    public TUserProfile Profile { get; set; } = default!;
}
