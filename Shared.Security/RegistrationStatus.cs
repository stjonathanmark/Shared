namespace Shared.Security;

public enum RegistrationStatus
{
    NotRegistered,
    InvalidUsernameFormat,
    UsernameAlreadyExists,
    InvalidEmailFormat,
    EmailAlreadyExists,
    Error,
    InvalidPasswordFormat,
    Registered
}
