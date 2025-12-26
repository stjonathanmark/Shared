namespace Shared.Security.Authentication;

public interface IAuthenticationService<TUser, TKey>
{
    Task<RegistrationResult> RegisterUserAsync(TUser user, string password, string? role = null);

    Task<ConfirmEmailResult> ConfirmEmailAsync(TKey userId, string token);

    Task<AuthenticationResult> AuthenticateUserAsync(string username, string password);

    Task<ChangePasswordResult> ChangePasswordAsync(TKey userId, string currentPassword, string newPassword);

    Task<RecoverPasswordResult> RecoverPasswordAsync(string username, string? email = null);

    Task<ResetPasswordResult> ResetPasswordAsync(TKey userId, string token, string newPassword);

    Task<ResendConfirmationEmailResult> ResendConfirmationEmailAsync(TKey userId);
}