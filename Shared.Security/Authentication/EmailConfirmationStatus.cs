namespace Shared.Security.Authentication;

public enum EmailConfirmationStatus
{
    NotConfirmed,
    UserDoesNotExist,
    InvalidToken,
    Error,
    Confirmed
}
