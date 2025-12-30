using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared.Mailing;
using System.Net;
using System.Text;

namespace Shared.Security.Authentication;

public class AuthenticationService<TUser, TKey> : IAuthenticationService<TUser, TKey>
    where TKey : IEquatable<TKey>
    where TUser : BaseUser<TKey>

{
    private readonly UserManager<TUser> userMgr;
    private readonly AuthenticationOptions authOpts;
    private readonly IEmailService smtp;

    public AuthenticationService(UserManager<TUser> userManager, IOptions<AuthenticationOptions> authenticationOptions, IEmailService emailService)
    {
        userMgr = userManager;
        authOpts = authenticationOptions.Value;
        smtp = emailService;
    }

    public async Task<CreateUserResult> CreateUserAsync(TUser user, string password, string? role = null)
    {
        var result = new CreateUserResult();

        try
        {
            var regResult = await userMgr.CreateAsync(user, password);

            if (!(result.Successful = regResult.Succeeded))
            {
                result.Message = "Error(s) occurred creating user";

                foreach (var error in regResult.Errors)
                    result.Data.Add(error.Code, error.Description);


                Dictionary<CreateUserStatus, string> statusToError = new() {
                    { CreateUserStatus.UsernameAlreadyExists, IdentityErrors.DuplicateUserName },
                    { CreateUserStatus.InvalidUsernameFormat, IdentityErrors.InvalidUserName },
                    { CreateUserStatus.EmailAlreadyExists, IdentityErrors.DuplicateEmail },
                    { CreateUserStatus.InvalidEmailFormat, IdentityErrors.InvalidEmail },
                    { CreateUserStatus.InvalidPasswordFormat, GetJoinedInvalidPasswordFormatErrors() },
                };

                result.Status = CreateUserStatus.Error;

                foreach (var kvPair in statusToError)
                {
                    var matchFound = kvPair.Key == CreateUserStatus.InvalidPasswordFormat
                        ? result.Data.Any(d => kvPair.Value.Contains(d.Key))
                        : result.Data.ContainsKey(kvPair.Value);

                        if (matchFound)
                        {
                            result.Status = kvPair.Key;
                            break;
                        }
                }
            }

            var addRoleResult = await userMgr.AddToRoleAsync(user, authOpts.DefaultRole ?? role ?? "User");

            if (!(result.Successful = addRoleResult.Succeeded))
            {
                foreach (var error in addRoleResult.Errors)
                    result.Data.Add(error.Code, error.Description);

                result.Status = CreateUserStatus.Error;
            }

            if (result.Successful && authOpts.RequireEmailConfirmation)
                await SendConfirmationEmailAsync(user);

            result.Status = CreateUserStatus.Created;
            result.Message = "User was created successfuly.";
        }
        catch
        {
            result.Successful = false;
            result.Status = CreateUserStatus.Error;
            return result;
        }

        return result;
    }

    public async Task<ConfirmEmailResult> ConfirmEmailAsync(TKey userId, string token)
    {
        var result = new ConfirmEmailResult();

        try
        {
            var user = await userMgr.FindByIdAsync(userId.ToString()!);

            if (user == null)
            {
                result.Successful = false;
                result.Message = "An error occurred confirming email.";
                result.Status = EmailConfirmationStatus.UserDoesNotExist;

                return result;
            }

            var originalToken = DecodeToken(token);

            var confirmEmailResult = await userMgr.ConfirmEmailAsync(user, originalToken);

            if (!(result.Successful = confirmEmailResult.Succeeded))
            {
                foreach (var error in confirmEmailResult.Errors)
                    result.Data.Add(error.Code, error.Description);

                if (result.Data.ContainsKey(IdentityErrors.InvalidToken))
                    result.Status = EmailConfirmationStatus.InvalidToken;
                else
                    result.Status = EmailConfirmationStatus.Error;

                return result;
            }

            result.Message = "Email was confirmed successfully.";
            result.Status = EmailConfirmationStatus.Confirmed;
        } 
        catch
        {
            result.Message = "An error occurred confirming email.";
            result.Successful = false;
            result.Status = EmailConfirmationStatus.Error;
        }

        return result;
    }

    public async Task<AuthenticationResult> AuthenticateUserAsync(string username, string password)
    {
        var result = new AuthenticationResult();

        try
        {
            var user = await userMgr.FindByNameAsync(username);

            if (user == null)
            {
                result.Successful = false;
                result.Status = AuthenticationStatus.UserNotFound;
                result.Message = "Invalid credentials have been provided, please try again.";
                return result;
            }

            var validPwd = await userMgr.CheckPasswordAsync(user, password);

            if (!(result.Successful = validPwd))
            {
                result.Status = AuthenticationStatus.InvalidPassword;
                result.Message = "Invalid credentials have been provided, please try again.";
                return result;
            }

            if (result.Successful && authOpts.RequireEmailConfirmation)
            {
                var emailConfirmed = await userMgr.IsEmailConfirmedAsync(user);
                if (!(result.Successful = emailConfirmed))
                {
                    result.Status = AuthenticationStatus.EmailNotConfirmed;
                    result.Message = "Email has not been confirmed for this account.";
                    return result;
                }
            }

            result.Status = AuthenticationStatus.Authenticated;
            result.Message = "User was authenticated successfully.";

        }
        catch
        {
            result.Successful = false;
            result.Message = "An error occurred authenticating user.";
            result.Status = AuthenticationStatus.Error;
        }

        return result;
    }

    public async Task<ChangePasswordResult> ChangePasswordAsync(TKey userId, string currentPassword, string newPassword)
    {
        var result = new ChangePasswordResult();

        try
        {
            var user = await userMgr.FindByIdAsync(userId.ToString()!);

            if (user == null)
            {
                result.Successful = false;
                result.Status = ChangePasswordStatus.UserDoesNotExist;
                result.Message = "User for password does not exist.";

                return result;
            }

            var changePwdResult = await userMgr.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!(result.Successful = changePwdResult.Succeeded))
            {
                foreach (var error in changePwdResult.Errors)
                    result.Data.Add(error.Code, error.Description);

                var pwdFormatErrs = GetJoinedInvalidPasswordFormatErrors();

                if (result.Data.Any(d => pwdFormatErrs.Contains(d.Key)))
                    result.Status = ChangePasswordStatus.InvalidFormat;
                else if (result.Data.ContainsKey(IdentityErrors.PasswordMismatch))
                    result.Status = ChangePasswordStatus.InvalidOldPassword;
                else
                    result.Status = ChangePasswordStatus.Error;

                result.Message = "Error(s) occurred changing password";

                return result;
            }

            result.Successful = true;
            result.Status = ChangePasswordStatus.Changed;
            result.Message = "Password was changed successfully.";
        }
        catch
        {
            result.Successful = false;
            result.Message = "An error occurred changing password.";
            result.Status = ChangePasswordStatus.Error;
        }

        return result;
    }

    public async Task<RecoverPasswordResult> RecoverPasswordAsync(string username, string? email = null)
    {
        var result = new RecoverPasswordResult();

        try
        {
            var user = await userMgr.FindByNameAsync(username);

            if (user == null)
            {
                result.Successful = false;
                result.Message = "User does not exist for password recovery.";
                result.Status = RecoverPasswordStatus.UserDoesNotExist;
            }

            var token = await userMgr.GeneratePasswordResetTokenAsync(user!);

            var encodedToken = EncodeToken(token);

            var url = string.Format(authOpts.Password.RecoverPasswordUrl, user!.Id, encodedToken);

            var template = smtp.GetEmailTemplate(authOpts.Password.RecoverPasswordTemplateName);
            template.AddReplacementValue("url", url);

            smtp.Send(template);

            result.Successful = true;
            result.Message = "Email has been sent to recover password.";
            result.Status = RecoverPasswordStatus.Recovered;

        }
        catch
        {
            result.Successful = true;
            result.Message = "An error occurred recovering password. Please try again.";
            result.Status = RecoverPasswordStatus.Error;
        }

        return result;
    }

    public async Task<ResetPasswordResult> ResetPasswordAsync(TKey userId, string token, string newPassword)
    {
        var result = new ResetPasswordResult();

        try
        {
            var user = await userMgr.FindByIdAsync(userId.ToString()!);

            if (user == null)
            {
                result.Successful = false;
                result.Message = "User does not exist for password reset.";
                result.Status = ResetPasswordStatus.UserDoesNotExist;

                return result;
            }

            var originalToken = DecodeToken(token);

            var resetPwdResult = await userMgr.ResetPasswordAsync(user, originalToken, newPassword);

            if (!(result.Successful = resetPwdResult.Succeeded))
            {
                foreach (var error in resetPwdResult.Errors)
                    result.Data.Add(error.Code, error.Description);


                var pwdFormatErrs = GetJoinedInvalidPasswordFormatErrors();

                if (result.Data.Any(d => pwdFormatErrs.Contains(d.Key)))
                    result.Status = ResetPasswordStatus.InvalidFormat;
                else if (result.Data.ContainsKey(IdentityErrors.InvalidToken))
                    result.Status = ResetPasswordStatus.InvalidToken;
                else
                    result.Status = ResetPasswordStatus.Error;

                result.Message = "Error(s) occurred resetting password";

                return result;
            }

            result.Successful = true;
            result.Message = "Password has been reset successfully.";
            result.Status = ResetPasswordStatus.Reset;
        }
        catch
        {
            result.Successful = false;
            result.Message = "An error occurred resetting password.";
            result.Status = ResetPasswordStatus.Error;
        }

        return result;
    }

    public async Task<ResendConfirmationEmailResult> ResendConfirmationEmailAsync(TKey userId)
    {
        var result = new ResendConfirmationEmailResult();

        try
        {
            var user = await userMgr.FindByIdAsync(userId.ToString()!);

            if (user == null)
            {
                result.Successful = false;
                result.Message = $"User with id '{userId}' does not exist.";
                result.Status = ResendConfirmationEmailStatus.UserDoesNotExist;

                return result;
            }

            await SendConfirmationEmailAsync(user);

            result.Successful = true;
            result.Status = ResendConfirmationEmailStatus.Sent;
            result.Message = "Confirmation email was reset successfully.";
        }
        catch
        {
            result.Successful = false;
            result.Status = ResendConfirmationEmailStatus.Sent;
            result.Message = "An error occurred resending confirmaiton email.";
        }

        return result;
    }

    protected virtual async Task SendConfirmationEmailAsync(TUser user)
    {
        var token = await userMgr.GenerateEmailConfirmationTokenAsync(user);

        var encodedToken = EncodeToken(token);

        var url = string.Format(authOpts.EmailConfirmationUrl, user.Id, encodedToken);

        var template = smtp.GetEmailTemplate(authOpts.EmailConfirmationTemplateName);
        template.AddReplacementValue("url", url);

        smtp.Send(template);
    }

    protected virtual string EncodeToken(string token)
    {
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        var base64TokenStr = Convert.ToBase64String(tokenBytes);
        return WebUtility.UrlEncode(base64TokenStr);
    }

    protected virtual string DecodeToken(string token)
    {
        var base64UrlTokenStr = WebUtility.UrlDecode(token);
        var tokenBytes = Convert.FromBase64String(base64UrlTokenStr);
        return Encoding.UTF8.GetString(tokenBytes);
    }

    protected virtual string GetJoinedInvalidPasswordFormatErrors()
    {
        List<string> invalidPwdFormatErrCodes = [
            IdentityErrors.PasswordTooShort,
            IdentityErrors.PasswordRequiresUniqueChar,
            IdentityErrors.PasswordRequiresNonAlphanumeric,
            IdentityErrors.PasswordRequiresDigit,
            IdentityErrors.PasswordRequiresLower,
            IdentityErrors.PasswordRequiresUpper
        ];

        return string.Join(',', invalidPwdFormatErrCodes);
    }
} 
