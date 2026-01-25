namespace Shared.Security;

public class SecurityUserProfile<TUser, TAddress, TUserId> : BaseUserProfile<ulong>
    where TUserId : struct, IEquatable<TUserId>
    where TUser : BaseUser<TUserId>, new()
    where TAddress : SecurityUserAddress<TUserId>, new()
{

    public override string EmailAddress
    {
        get => User.Email!;
        set
        {
            User.Email = value;
            User.NormalizedEmail = value.ToUpper();
        }
    }

    public override string PhoneNumber { get => User.PhoneNumber!; set => User.PhoneNumber = value; }

    public TAddress Address { get; set; } = new();

    public TUser User { get; set; } = new();
}
