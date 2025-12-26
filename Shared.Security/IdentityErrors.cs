namespace Shared.Security
{
    /// <summary>
    /// Compile-time constants for ASP.NET Identity error codes.
    /// Property names match the IdentityError.Code values.
    /// </summary>
    public static class IdentityErrors
    {
        public const string DefaultError = "DefaultError";
        public const string ConcurrencyFailure = "ConcurrencyFailure";
        public const string PasswordMismatch = "PasswordMismatch";
        public const string InvalidToken = "InvalidToken";
        public const string LoginAlreadyAssociated = "LoginAlreadyAssociated";

        public const string InvalidUserName = "InvalidUserName";
        public const string InvalidEmail = "InvalidEmail";
        public const string DuplicateUserName = "DuplicateUserName";
        public const string DuplicateEmail = "DuplicateEmail";

        public const string InvalidRoleName = "InvalidRoleName";
        public const string DuplicateRoleName = "DuplicateRoleName";

        public const string UserAlreadyHasPassword = "UserAlreadyHasPassword";
        public const string UserLockoutNotEnabled = "UserLockoutNotEnabled";
        public const string UserAlreadyInRole = "UserAlreadyInRole";
        public const string UserNotInRole = "UserNotInRole";

        public const string PasswordTooShort = "PasswordTooShort";
        public const string PasswordTooLong = "PasswordTooLong";
        public const string PasswordRequiresNonAlphanumeric = "PasswordRequiresNonAlphanumeric";
        public const string PasswordRequiresDigit = "PasswordRequiresDigit";
        public const string PasswordRequiresLower = "PasswordRequiresLower";
        public const string PasswordRequiresUpper = "PasswordRequiresUpper";
        public const string PasswordRequiresUniqueChars = "PasswordRequiresUniqueChars";
        // Keep singular variant too because some code in the repo uses it
        public const string PasswordRequiresUniqueChar = "PasswordRequiresUniqueChar";

        public const string RecoveryCodeRedemptionFailed = "RecoveryCodeRedemptionFailed";
        public const string InvalidRecoveryCode = "InvalidRecoveryCode";
    }
}