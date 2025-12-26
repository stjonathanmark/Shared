namespace Shared.Security.Authentication;

public class ConfirmEmailResult : BaseResult
{
    public EmailConfirmationStatus Status { get; set; }
}
