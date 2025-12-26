namespace Shared.Security.Authentication;

public enum ResetPasswordStatus
{
    NotReset,
    UserDoesNotExist,
    InvalidFormat,
    InvalidToken,
    Reused,
    Error,
    Reset
}
