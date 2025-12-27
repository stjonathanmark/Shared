namespace Shared.Security.Authentication;

public enum CreateUserStatus
{
    NotCreated,
    InvalidUsernameFormat,
    UsernameAlreadyExists,
    InvalidEmailFormat,
    EmailAlreadyExists,
    Error,
    InvalidPasswordFormat,
    Created
}
