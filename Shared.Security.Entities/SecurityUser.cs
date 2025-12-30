namespace Shared.Security;

public class SecurityUser<TUserProfile, TProfileId> : BaseUser<string>
    where TUserProfile : BaseUserProfile<TProfileId, string>
    where TProfileId : struct
{
    public TUserProfile Profile { get; set; } = default!;
}
