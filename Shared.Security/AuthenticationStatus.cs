namespace Shared.Security;

public enum AuthenticationStatus
{
    NotAuthenticated,
    UserNotFound,
    InvalidPassword,
    EmailNotConfirmed,
    TemporaryPassword,
    PasswordExpired,
    LockedOut,
    Error,
    Authenticated
}
