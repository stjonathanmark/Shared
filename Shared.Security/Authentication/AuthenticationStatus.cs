namespace Shared.Security.Authentication;

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
