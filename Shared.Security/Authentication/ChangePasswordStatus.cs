namespace Shared.Security.Authentication;

public enum ChangePasswordStatus
{
    NotChanged,
    UserDoesNotExist,
    InvalidFormat,
    InvalidOldPassword,
    Reused,
    Error,
    Changed
}
